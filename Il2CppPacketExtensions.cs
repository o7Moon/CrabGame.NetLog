using UnityEngine;

public static class Il2CppPacketExtensions {
    public static void SetBytes(this Il2CppPacket packet, byte[] bytes){
        packet.Method_Public_Void_ArrayOf_Byte_0(bytes);
    }
    public static Packet toPacket(this Il2CppPacket packet){
        return new Packet(packet.field_Private_List_1_Byte_0.ToArray());
    }
    public static short ReadShort(this Il2CppPacket packet, bool move = true){
        return packet.Method_Public_Int16_Boolean_PDM_0(move);
    }
    public static int ReadInt(this Il2CppPacket packet, bool move = true){
        return packet.Method_Public_Int32_Boolean_0(move);
    }
    public static long ReadLong(this Il2CppPacket packet, bool move = true){
        return packet.Method_Public_Int64_Boolean_PDM_0(move);
    }
    public static ulong ReadULong(this Il2CppPacket packet, bool move = true){
        return packet.Method_Public_UInt64_Boolean_0(move);
    }
    public static float ReadFloat(this Il2CppPacket packet, bool move = true){
        return packet.Method_Public_Single_Boolean_0(move);
    }
    public static bool ReadBool(this Il2CppPacket packet, bool move = true){
        return packet.Method_Public_Boolean_Boolean_0(move);
    }
    public static string ReadString(this Il2CppPacket packet, bool move = true){
        return packet.Method_Public_String_Boolean_0(move);
    }
    public static Vector3 ReadVector3(this Il2CppPacket packet, bool move = true){
        return packet.Method_Public_Vector3_Boolean_0(move);
    }
    public static Quaternion ReadQuaternion(this Il2CppPacket packet, bool move = true){
        return packet.Method_Public_Quaternion_Boolean_0(move);
    }
}