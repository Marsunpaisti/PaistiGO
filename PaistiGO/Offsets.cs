using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace PaistiGO
{
    public static class HazeDownloader
    {
        public static OffsetObject ReadOffsetFile()
        {
            string jsonfile = File.ReadAllText($@"{System.AppDomain.CurrentDomain.BaseDirectory}\csgo.json");
            OffsetObject offsets = JsonConvert.DeserializeObject<OffsetObject>(jsonfile);
            return offsets;
        }

        public static void DownloadDump()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("https://raw.githubusercontent.com/frk1/hazedumper/master/csgo.json");
            string webData = Encoding.UTF8.GetString(raw);
            File.WriteAllText($@"{System.AppDomain.CurrentDomain.BaseDirectory}\csgo.json", webData);
            ReadOffsetFile();
        }
    }

    public class Signatures
    {
        public Int32 clientstate_choked_commands { get; set; }
        public Int32 clientstate_delta_ticks { get; set; }
        public Int32 clientstate_last_outgoing_command { get; set; }
        public Int32 clientstate_net_channel { get; set; }
        public Int32 convar_name_hash_table { get; set; }
        public Int32 dwClientState { get; set; }
        public Int32 dwClientState_GetLocalPlayer { get; set; }
        public Int32 dwClientState_IsHLTV { get; set; }
        public Int32 dwClientState_Map { get; set; }
        public Int32 dwClientState_MapDirectory { get; set; }
        public Int32 dwClientState_MaxPlayer { get; set; }
        public Int32 dwClientState_PlayerInfo { get; set; }
        public Int32 dwClientState_State { get; set; }
        public Int32 dwClientState_ViewAngles { get; set; }
        public Int32 dwEntityList { get; set; }
        public Int32 dwForceAttack { get; set; }
        public Int32 dwForceAttack2 { get; set; }
        public Int32 dwForceBackward { get; set; }
        public Int32 dwForceForward { get; set; }
        public Int32 dwForceJump { get; set; }
        public Int32 dwForceLeft { get; set; }
        public Int32 dwForceRight { get; set; }
        public Int32 dwGameDir { get; set; }
        public Int32 dwGameRulesProxy { get; set; }
        public Int32 dwGetAllClasses { get; set; }
        public Int32 dwGlobalVars { get; set; }
        public Int32 dwGlowObjectManager { get; set; }
        public Int32 dwInput { get; set; }
        public Int32 dwInt32erfaceLinkList { get; set; }
        public Int32 dwLocalPlayer { get; set; }
        public Int32 dwMouseEnable { get; set; }
        public Int32 dwMouseEnablePtr { get; set; }
        public Int32 dwPlayerResource { get; set; }
        public Int32 dwRadarBase { get; set; }
        public Int32 dwSensitivity { get; set; }
        public Int32 dwSensitivityPtr { get; set; }
        public Int32 dwSetClanTag { get; set; }
        public Int32 dwViewMatrix { get; set; }
        public Int32 dwWeaponTable { get; set; }
        public Int32 dwWeaponTableIndex { get; set; }
        public Int32 dwYawPtr { get; set; }
        public Int32 dwZoomSensitivityRatioPtr { get; set; }
        public Int32 dwbSendPackets { get; set; }
        public Int32 dwppDirect3DDevice9 { get; set; }
        public Int32 Int32erface_engine_cvar { get; set; }
        public Int32 m_bDormant { get; set; }
        public Int32 m_pStudioHdr { get; set; }
        public Int32 m_pitchClassPtr { get; set; }
        public Int32 m_yawClassPtr { get; set; }
        public Int32 model_ambient_min { get; set; }
    }

    public class Netvars
    {
        public Int32 cs_gamerules_data { get; set; }
        public Int32 m_ArmorValue { get; set; }
        public Int32 m_Collision { get; set; }
        public Int32 m_CollisionGroup { get; set; }
        public Int32 m_Local { get; set; }
        public Int32 m_MoveType { get; set; }
        public Int32 m_OriginalOwnerXuidHigh { get; set; }
        public Int32 m_OriginalOwnerXuidLow { get; set; }
        public Int32 m_SurvivalGameRuleDecisionTypes { get; set; }
        public Int32 m_SurvivalRules { get; set; }
        public Int32 m_aimPunchAngle { get; set; }
        public Int32 m_aimPunchAngleVel { get; set; }
        public Int32 m_bBombPlanted { get; set; }
        public Int32 m_bFreezePeriod { get; set; }
        public Int32 m_bGunGameImmunity { get; set; }
        public Int32 m_bHasDefuser { get; set; }
        public Int32 m_bHasHelmet { get; set; }
        public Int32 m_bInReload { get; set; }
        public Int32 m_bIsDefusing { get; set; }
        public Int32 m_bIsQueuedMatchmaking { get; set; }
        public Int32 m_bIsScoped { get; set; }
        public Int32 m_bIsValveDS { get; set; }
        public Int32 m_bSpotted { get; set; }
        public Int32 m_bSpottedByMask { get; set; }
        public Int32 m_clrRender { get; set; }
        public Int32 m_dwBoneMatrix { get; set; }
        public Int32 m_fAccuracyPenalty { get; set; }
        public Int32 m_fFlags { get; set; }
        public Int32 m_flC4Blow { get; set; }
        public Int32 m_flDefuseCountDown { get; set; }
        public Int32 m_flDefuseLength { get; set; }
        public Int32 m_flFallbackWear { get; set; }
        public Int32 m_flFlashDuration { get; set; }
        public Int32 m_flFlashMaxAlpha { get; set; }
        public Int32 m_flNextPrimaryAttack { get; set; }
        public Int32 m_flTimerLength { get; set; }
        public Int32 m_hActiveWeapon { get; set; }
        public Int32 m_hMyWeapons { get; set; }
        public Int32 m_hObserverTarget { get; set; }
        public Int32 m_hOwner { get; set; }
        public Int32 m_hOwnerEntity { get; set; }
        public Int32 m_iAccountID { get; set; }
        public Int32 m_iClip1 { get; set; }
        public Int32 m_iCompetitiveRanking { get; set; }
        public Int32 m_iCompetitiveWins { get; set; }
        public Int32 m_iCrosshairId { get; set; }
        public Int32 m_iEntityQuality { get; set; }
        public Int32 m_iFOV { get; set; }
        public Int32 m_iFOVStart { get; set; }
        public Int32 m_iGlowIndex { get; set; }
        public Int32 m_iHealth { get; set; }
        public Int32 m_iItemDefinitionIndex { get; set; }
        public Int32 m_iItemIDHigh { get; set; }
        public Int32 m_iObserverMode { get; set; }
        public Int32 m_iShotsFired { get; set; }
        public Int32 m_iState { get; set; }
        public Int32 m_iTeamNum { get; set; }
        public Int32 m_lifeState { get; set; }
        public Int32 m_nFallbackPaInt32Kit { get; set; }
        public Int32 m_nFallbackSeed { get; set; }
        public Int32 m_nFallbackStatTrak { get; set; }
        public Int32 m_nForceBone { get; set; }
        public Int32 m_nTickBase { get; set; }
        public Int32 m_rgflCoordinateFrame { get; set; }
        public Int32 m_szCustomName { get; set; }
        public Int32 m_szLastPlaceName { get; set; }
        public Int32 m_thirdPersonViewAngles { get; set; }
        public Int32 m_vecOrigin { get; set; }
        public Int32 m_vecVelocity { get; set; }
        public Int32 m_vecViewOffset { get; set; }
        public Int32 m_viewPunchAngle { get; set; }
    }

    public class OffsetObject
    {
        public int timestamp { get; set; }
        public Signatures signatures { get; set; }
        public Netvars netvars { get; set; }
    }
}
