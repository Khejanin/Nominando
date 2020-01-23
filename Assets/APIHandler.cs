using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class APIHandler : MonoBehaviour
{
    private static APIHandler apiHandler;
    
    private static string token = "";
    private string enemyString = "";
    private bool tokenSet = false;

    public List<Texture2D> textureLibrary = new List<Texture2D>();

    private string ip = "khejani.ga:3000";

    public GameInitializer GameInitializer;

    public static APIHandler getAPIHandler()
    {
        return apiHandler;
    }

    public IEnumerator Login(string username, string password)
    {
        JSONUser user = new JSONUser();
        user.name = username;
        user.password = password;
        
        var bodyData = JsonUtility.ToJson(user,true);
        var postData = System.Text.Encoding.UTF8.GetBytes(bodyData);

        UnityWebRequest request = UnityWebRequest.Post(ip+"/login", UnityWebRequest.kHttpVerbPOST);
        UploadHandlerRaw uH= new UploadHandlerRaw(postData);
        request.uploadHandler = uH;
        request.SetRequestHeader("Content-Type","application/json");
        
        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (!request.downloadHandler.text.Equals("Could not authenticate player"))
            {
                token = request.downloadHandler.text;
                tokenSet = true;
                LoginEventInfo loginEvent = ScriptableObject.CreateInstance<LoginEventInfo>();
                loginEvent.eventState = LoginEventInfo.LoginEventState.LOGIN_SUCCESSFUL;
                EventSystem.EventSystem.FireEvent(loginEvent);
                Debug.Log("Successfully logged in, Token : " + token);
            }
            else{
                NotificationTextScript.GetNotificationTextScript().SetNotificationTextAndShow("Invalid Login Data");
            }
        }
    }
    
    public void Logout()
    {
        token = "";
        LoginEventInfo eventInfo = ScriptableObject.CreateInstance<LoginEventInfo>();
        eventInfo.eventState = LoginEventInfo.LoginEventState.LOGOUT;
        EventSystem.EventSystem.FireEvent(eventInfo);
        GameInitializer.ClearAll();
    }

    public void ClearData()
    {
        StartCoroutine(clearData());
    }
    
    private IEnumerator clearData()
    {
        UnityWebRequest request = UnityWebRequest.Delete(ip+"/entities");
        request.SetRequestHeader("Authorization",token);
        
        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
           NotificationTextScript.GetNotificationTextScript().SetNotificationTextAndShow("All Data has been cleared successfully");
           Logout();
        }
    }

    public IEnumerator Register(string username, string password)
    {
        JSONUser user = new JSONUser();
        user.name = username;
        user.password = password;
        
        
        var bodyData = JsonUtility.ToJson(user,true);
        var postData = System.Text.Encoding.UTF8.GetBytes(bodyData);

        UnityWebRequest request = UnityWebRequest.Post(ip+"/register", UnityWebRequest.kHttpVerbPOST);
        UploadHandlerRaw uH= new UploadHandlerRaw(postData);
        request.uploadHandler = uH;
        request.SetRequestHeader("Content-Type","application/json");
        
        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            token = request.downloadHandler.text;
            tokenSet = true;
            LoginEventInfo loginEvent = ScriptableObject.CreateInstance<LoginEventInfo>();
            loginEvent.eventState = LoginEventInfo.LoginEventState.REGISTRATION_SUCCESSFUL;
            NotificationTextScript.GetNotificationTextScript().SetNotificationTextAndShow("Registration successful! The game will start now!");
            EventSystem.EventSystem.FireEvent(loginEvent);
        }
    }

    IEnumerator GetEntities()
    {
        UnityWebRequest request = UnityWebRequest.Get(ip+"/entities");
        request.SetRequestHeader("Authorization",token);
        
        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            enemyString = request.downloadHandler.text;
            
            var serverData = JsonConvert.DeserializeObject<ServerData>(enemyString);

            GameInitializer.InitializeGame(serverData);
        }
    }


    public void UploadEntityData(EntityUploadJSON entityUploadJson)
    {
        StartCoroutine(uploadEntityData(entityUploadJson));
    }
    
    private IEnumerator uploadEntityData(EntityUploadJSON entityUploadJson)
    {
        string data = JsonConvert.SerializeObject(entityUploadJson);

        byte[] bytes = Encoding.ASCII.GetBytes(data);
        
        UnityWebRequest request = UnityWebRequest.Post(ip+"/entity",UnityWebRequest.kHttpVerbPOST);
        
        UploadHandlerRaw uH= new UploadHandlerRaw(bytes);
        request.uploadHandler= uH;
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization",token);
        
        //request.SetRequestHeader("Content-Type","application/json");
        
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.data);
        }
    }

    public void UploadEntityImage(Namable.Namable nam, Texture2D tex)
    {
        StartCoroutine(uploadEntityImage(nam, tex));
    }

    public IEnumerator uploadEntityImage(Namable.Namable nam, Texture2D tex)
    {
        byte[] pngBytes = tex.EncodeToPNG();
        
        WWWForm form = new WWWForm();
        form.AddBinaryData("picture",pngBytes,"picture.png","image/png");
        
        UnityWebRequest request = UnityWebRequest.Post(ip+"/upload",form);
        
        request.SetRequestHeader("Authorization",token);
        request.SetRequestHeader("EntityID",nam.uniqueID);
        form.headers.Add("Authorization",token);
        form.headers.Add("EntityID",nam.uniqueID);
        request.uploadHandler.contentType = "multipart/form-data";
        
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            nam.imagePath = request.downloadHandler.text;
        }
    }

    public void FetchImage(string url, FetchImageCallback cb)
    {
        StartCoroutine(fetchImage(url, cb));
    }
    private IEnumerator fetchImage(string url,FetchImageCallback cb)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(ip+"/images/"+url);
        request.SetRequestHeader("Authorization",token);

        yield return request.SendWebRequest();

        while (!request.downloadHandler.isDone)
        {
            yield return request;
        }
        
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Texture2D texture = (request.downloadHandler as DownloadHandlerTexture).texture;
            textureLibrary.Add(texture);
            cb.Invoke(texture);
        }
    }

    public delegate void FetchImageCallback(Texture2D texture);

    private void Start()
    {
        apiHandler = this;
        EventSystem.EventSystem.RegisterListener<LoginEventInfo>(HandleLoginEvent);
        GameInitializer = gameObject.GetComponent<GameInitializer>();
        GameInitializer.ClearAll();
    }

    public void HandleLoginEvent(LoginEventInfo loginEventInfo)
    {
        switch (loginEventInfo.eventState)
        {
            case LoginEventInfo.LoginEventState.LOGIN_SUCCESSFUL:
                StartCoroutine(GetEntities());
                break;
            case LoginEventInfo.LoginEventState.LOGIN_FAILED:
                break;
            case LoginEventInfo.LoginEventState.REGISTRATION_SUCCESSFUL:
                StartCoroutine(GetEntities());
                GameInitializer.ClearAll();
                GameInitializer.StartGameFirstTime();
                break;
            case LoginEventInfo.LoginEventState.REGISTRATION_FAILED:
                break;
        }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string newJson = "{ \"array\": " + json + "}";
            JToken jToken = JToken.Parse(newJson);
            Wrapper<T> wrapper = jToken.ToObject<Wrapper<T>>();
            return wrapper.array;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] array;
        }
    }

    [System.Serializable]
    public class Enemy
    {
        public string _id;
        public string name;
        public int health;
        public int positionX;
        public int positionY;
    }

    [Serializable]
    public class JSONUser
    {
        [SerializeField]
        public string name;

        [SerializeField]
        public string password;

    }

    public class EntityUploadJSON
    {
        [JsonProperty("isNamable")] public bool isNamable;
        
        [JsonProperty("uniqueID")] public string UniqueId;

        [JsonProperty("name")] public string Name;

        [JsonProperty("state")] public string State;

        [JsonProperty("dialogueState")] public int dialogueState;

        [JsonProperty("hideLocationNr")]
        public int HideLocationNr;

        [JsonProperty("hideEntityNr")]
        public int HideEntityNr;

        [JsonProperty("hideActionNr")]
        public int HideActionNr;
    }
    
    public partial class ServerData
    {
        [JsonProperty("progress")]
        //  [JsonConverter(typeof(ParseStringConverter))]
        public long Progress;

      [JsonProperty("namables")] public NamableJSON[] Namables;

      [JsonProperty("locations")] public Location[] Locations;
    }

    public partial class Location
    {
        [JsonProperty("uniqueID")] public string UniqueId;

        [JsonProperty("name")] public string Name;

        [JsonProperty("state")] public string State;

        [JsonProperty("imagePath")] public string ImagePath;

        [JsonProperty("hideLocationNr")]
        //   [JsonConverter(typeof(ParseStringConverter))]
        public int HideLocationNr;

     [JsonProperty("hideEntityNr")]
     // [JsonConverter(typeof(ParseStringConverter))]
     public int HideEntityNr;

       [JsonProperty("hideActionNr")]
       //  [JsonConverter(typeof(ParseStringConverter))]
       public int HideActionNr;
    }

    public partial class NamableJSON
    {
        [JsonProperty("uniqueID")] public string UniqueId;

        [JsonProperty("name")] public string Name;

        [JsonProperty("state")] public string State;

        [JsonProperty("dialogueState")] public int dialogueState;

        [JsonProperty("imagePath")] public string ImagePath;
    }
}
