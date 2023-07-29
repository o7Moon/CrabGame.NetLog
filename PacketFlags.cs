using System;

[Flags]
public enum PacketFlags {// https://partner.steamgames.com/doc/api/steamnetworkingtypes#message_sending_flags
    Unreliable = 0,
    NoNagle = 1, 
    NoDelay = 4,
    Reliable = 8,
}