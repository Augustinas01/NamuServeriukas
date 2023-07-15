﻿namespace Enums
{
    public class ServiceEnum
    {
        public enum Type
        {
            None,
            Undefined,
            Game,
            Software
        }
        public enum Name
        {
            None,
            Undefined,
            Factorio
        }
        public enum Action
        {
            ServiceStateChanged,
            ServiceSpecific,
            PlayerAction
        }
        public enum PlayerAction
        {
            Join,
            Leave
        }
    }
}