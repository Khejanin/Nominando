using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{ 
    public class LoginEventInfo : EventInfo
    {
        public LoginEventState eventState;

        public enum LoginEventState
        {
            LOGIN_SUCCESSFUL,
            LOGIN_FAILED,
            REGISTRATION_SUCCESSFUL,
            REGISTRATION_FAILED,
            LOGOUT,
            BACK_TO_MENU
        }
    }
}