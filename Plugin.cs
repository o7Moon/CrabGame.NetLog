using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using SteamworksNative;
using UnityEngine;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System;

namespace NetLog
{
    [BepInPlugin("CrabGame.NetLog", "NetLog", "1.0")]
    public class Plugin : BasePlugin
    {
        public static Plugin Instance;
        public static string logpath;
        public static FileStream logfile;
        public override void Load()
        {
            Instance = this;
            logpath = Directory.GetParent(Application.dataPath)+"packets.log";
            ClearLogfile();
            Harmony.CreateAndPatchAll(typeof(Plugin));
            Log.LogInfo("netlog is loaded!");
        }

        public static void ClearLogfile(){
            if (logfile != null) logfile.Close();
            File.WriteAllText(logpath,string.Empty);
            logfile = File.OpenWrite(logpath);
        }

        public static void LogRaw(string s){
            byte[] data = new UTF8Encoding(true).GetBytes(s);
            logfile.Write(data, 0, data.Length);
        }
        public static void LogLine(string s){
            LogRaw(s+Environment.NewLine);
        }
        public static void LogLine(){
            LogRaw(Environment.NewLine);
        }

        [HarmonyPatch(typeof(SteamPacketManager), nameof(SteamPacketManager.SendPacket))]
        [HarmonyPrefix]
        public static void onPacketSent(CSteamID __0, Il2CppPacket __1, int __2, NetworkChannel __3){
            logPacket(__0,__1.toPacket(),(PacketFlags)__2,__3);
        }

        [HarmonyPatch(typeof(SteamPacketManager), nameof(SteamPacketManager.Method_Private_Static_Void_SteamNetworkingMessage_t_Int32_0))]
        [HarmonyPrefix]
        public static void onPacketReceived(SteamNetworkingMessage_t __0, int __1){
            byte[] contents = new byte[__0.m_cbSize]; 
            Marshal.Copy(__0.m_pData, contents,0,__0.m_cbSize);
            logPacket(SteamManager.Instance.prop_CSteamID_0, new Packet(contents), PacketFlags.Reliable,NetworkChannel.ToClient, true);
        }

        public static void logPacket(CSteamID destination, Packet packet, PacketFlags flags, NetworkChannel channel, bool received = false){
            int ID = packet.ReadInt();
            string is_self = destination==SteamManager.Instance.prop_CSteamID_0 ? "(self)":"";
            string packet_sent_or_received = received?"received":$"sent going {channel.ToString()} {destination} {is_self}";
            string flags_string = received?"":$" and flags {flags.ToString()}";
            LogLine($"packet {packet_sent_or_received} with ID {ID}{flags_string}.");
            string methodstring = "handler method unknown";
            if (channel == NetworkChannel.ToClient && SteamPacketManager.ClientPacketHandlers.ContainsKey(ID)){
                methodstring = $"Client Handler: {SteamPacketManager.ClientPacketHandlers[ID].Method.Name}";
            } else if (channel == NetworkChannel.ToServer && SteamPacketManager.ServerPacketHandlers.ContainsKey(ID)){
                methodstring = $"Server Handler: {SteamPacketManager.ServerPacketHandlers[ID]}";
            }
            LogLine(methodstring);
            LogLine();
        }
        //public static void
    }
}
