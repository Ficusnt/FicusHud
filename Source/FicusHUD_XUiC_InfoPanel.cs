using System;
using UnityEngine;

namespace FicusHUD
{
    /// <summary>
    /// Controller for HUDLeftStatBars.
    ///
    /// Provides {ficus_*} bindings and sets Ficus_ CVars on the player.
    ///
    /// ── BINDINGS ──────────────────────────────────────────────────────────
    ///   ficus_worldday           — world day string
    ///   ficus_worldhour          — "HH:MM" string
    ///   ficus_playername         — player entity name
    ///   ficus_playerlevel        — player level
    ///   ficus_armorrating        — armor rating integer string
    ///   ficus_playerspeed        — "1.23x" string
    ///   ficus_gamestage          — game stage
    ///   ficus_lootstage          — loot stage (stubbed = GS)
    ///   ficus_skillpoints        — available skill points
    ///   ficus_isDaytime          — "true"/"false" — World.IsDaytime()
    ///   ficus_stormTimer         — real-time remaining string e.g. "12:30"
    ///   ficus_weatherType        — e.g. "None", "Snowy", "Stormy", "Rainy",
    ///                              "Foggy", "BloodMoon", "Biome"
    ///   ficus_notch1_color .. ficus_notch7_color  — pip sprite color per notch
    ///   ficus_notch1_lcolor .. ficus_notch7_lcolor — label color per notch
    ///
    ///   ficus_activeItemName     — STUB ""
    ///     dnSpy target: ItemClass.list[player.inventory.holdingItem.type]
    ///     Look for: GetLocalizedItemName() on ItemClass
    ///     File: Assembly-CSharp.dll → ItemClass
    ///
    ///   ficus_activeItemDurColor — STUB grey "180,180,180,220"
    ///     dnSpy target: player.inventory.holdingItem (ItemValue)
    ///     Fields: UseTimes (int, damage taken), MaxUseTimes (int, max)
    ///     Formula: fill = 1f - (float)UseTimes / MaxUseTimes
    ///     Color logic: fill &lt; 0.25 → "187,45,59,255" (red)
    ///                  fill &lt; 0.5  → "255,195,0,255" (yellow)
    ///                  else         → "180,180,180,220" (grey)
    ///     File: Assembly-CSharp.dll → ItemValue
    ///
    /// ── CVARS ─────────────────────────────────────────────────────────────
    ///   Ficus_xpFill        — STUB 0.5f
    ///     dnSpy target: player.Progression (EntityPlayerProgressionSDCS)
    ///     Fields: find Exp (current XP) and GetExpForLevel(int) or ExpToNextLevel
    ///     Formula: (Exp - ExpForLevel(Level)) /
    ///              (ExpForLevel(Level+1) - ExpForLevel(Level))
    ///     File: Assembly-CSharp.dll → EntityPlayerProgressionSDCS
    ///
    ///   Ficus_armorRating, Ficus_playerSpeed — CONFIRMED working
    ///   Ficus_playerGameStage, Ficus_playerLootStage — CONFIRMED working
    ///   Ficus_stormFill     — CONFIRMED: from BiomeWeather storm fields
    ///   Ficus_stormActive   — CONFIRMED: stormLevel > 0 ? 1f : 0f
    /// </summary>
    public class XUiC_InfoPanel : XUiController
    {
        // ── notch color constants ────────────────────────────────────────────
        private const string CLR_DIM     = "120,110,85,153";
        private const string CLR_ACTIVE  = "180,160,120,200";
        private const string CLR_BM      = "160,48,48,230";
        private const string LCLR_DIM    = "120,110,85,153";
        private const string LCLR_ACTIVE = "220,210,190,240";
        private const string LCLR_BM     = "200,60,60,230";

        // ── cached binding strings ───────────────────────────────────────────
        private string worldDay    = "1";
        private string worldHour   = "00:00";
        private string playerName  = "";
        private string playerLevel = "1";
        private string armorRating = "0";
        private string playerSpeed = "1.00x";
        private string gameStage   = "1";
        private string lootStage   = "1";
        private string skillPoints = "0";
        private string isDaytime   = "true";
        private string stormTimer  = "0:00";
        private string weatherType = "None";

        // Active item stubs — filled once dnSpy confirms ItemValue / ItemClass API
        private string activeItemName     = "";
        private string activeItemDurColor = "180,180,180,220";

        // Per-notch colors — index 1..7 used, 0 unused
        private readonly string[] notchColor  = { "", CLR_DIM, CLR_DIM, CLR_DIM, CLR_DIM, CLR_DIM, CLR_DIM, CLR_DIM };
        private readonly string[] notchLColor = { "", LCLR_DIM, LCLR_DIM,       LCLR_DIM, LCLR_DIM, LCLR_DIM, LCLR_DIM, LCLR_DIM };

        // ── internal state ───────────────────────────────────────────────────
        private EntityPlayerLocal localPlayer;
        private float updateTimer   = 0f;
        private const float UPDATE_INTERVAL = 0.5f;

        // ── lifecycle ────────────────────────────────────────────────────────
<
        public override void Init()
        {
            base.Init();
            IsDirty = true;
        }

        public override void OnOpen()
        {
            base.OnOpen();
            IsDirty = true;
        }

        public override void Update(float _dt)
        {
            try { base.Update(_dt); } catch { }

            try
            {
                if (localPlayer == null && XUi.IsGameRunning())
                    localPlayer = xui?.playerUI?.entityPlayer as EntityPlayerLocal;

                if (localPlayer == null)
                    return;

                updateTimer += _dt;
                if (updateTimer < UPDATE_INTERVAL && !IsDirty)
                    return;

                updateTimer = 0f;
                IsDirty     = false;

                UpdateValues();
                SetCVars();
                RefreshBindings(true);
            }
            catch (Exception e)
            {
                Debug.LogWarning("[FicusHUD] Update: " + e.Message);
            }
        }

        // ── value computation ────────────────────────────────────────────────

        private void UpdateValues()
        {
            try
            {
                // ── World time ──────────────────────────────────────────────
                ulong wTime   = GameManager.Instance.World.GetWorldTime();
                int   wDay    = (int)GameUtils.WorldTimeToDays(wTime);
                float rawHour = (float)GameUtils.WorldTimeToHours(wTime);
                int   hour    = (int)rawHour;
                int   minute  = Mathf.RoundToInt((rawHour - hour) * 60f);
                if (minute == 60) { hour++; minute = 0; }
                if (hour  == 24)   hour = 0;

                worldDay  = wDay.ToString();
                worldHour = string.Format("{0:D2}:{1:D2}", hour, minute);

                // isDaytime — confirmed: GameManager.Instance.World.IsDaytime()
                isDaytime = GameManager.Instance.World.IsDaytime().ToString();

                // ── Player stats ────────────────────────────────────────────
                playerName  = localPlayer.EntityName ?? "";
                if (localPlayer.Progression != null)
                {
                    playerLevel = localPlayer.Progression.Level.ToString();
                    skillPoints = localPlayer.Progression.SkillPoints.ToString();
                }
                gameStage   = localPlayer.gameStage.ToString();
                lootStage   = localPlayer.gameStage.ToString();

                // Armor — confirmed via EffectManager
                float armor = EffectManager.GetValue(
                    PassiveEffects.PhysicalDamageResist,
                    null, 0f, localPlayer, null,
                    default(FastTags<TagGroup.Global>),
                    true, true, true, true, true, 1, true, false);
                armorRating = ((int)armor).ToString();

                // Speed — confirmed via GetSpeedModifier
                playerSpeed = localPlayer.GetSpeedModifier().ToString("F2") + "x";

                // Active item — stub until dnSpy confirms fields
                activeItemName     = "";
                activeItemDurColor = "180,180,180,220";

                // ── Storm / Weather ─────────────────────────────────────────
                // Pattern confirmed from CATUI_XUiC_CompassWindowPatch.cs
                UpdateWeather(wDay);

                // ── Day cycle notch colors ──────────────────────────────────
                // Blood moon day: GameStats.GetInt(EnumGameStats.BloodMoonDay)
                // This returns the ABSOLUTE next blood moon day directly.
                // Much simpler than the old AIDirectorBloodMoonComponent approach.
                int cycleLen = GamePrefs.GetInt(EnumGamePrefs.BloodMoonFrequency);
                if (cycleLen < 1) cycleLen = 7;

                int dayInCycle = ((wDay - 1) % cycleLen) + 1;
                int bmAbsDay   = GameStats.GetInt(EnumGameStats.BloodMoonDay);
                int bmInCycle  = ((bmAbsDay - 1) % cycleLen) + 1;

                for (int i = 1; i <= 7; i++)
                {
                    if (i == bmInCycle)
                    {
                        notchColor[i]  = CLR_BM;
                        notchLColor[i] = LCLR_BM;
                    }
                    else if (i == dayInCycle)
                    {
                        notchColor[i]  = CLR_ACTIVE;
                        notchLColor[i] = LCLR_ACTIVE;
                    }
                    else
                    {
                        notchColor[i]  = CLR_DIM;
                        notchLColor[i] = LCLR_DIM;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("[FicusHUD] XUiC_InfoPanel.UpdateValues: " + e.Message);
            }
        }

        private void UpdateWeather(int wDay)
        {
            // All WeatherManager access confirmed from CATUI_XUiC_CompassWindowPatch.cs
            try
            {
                int currentWorldTime = WeatherManager.worldTime;

                WeatherManager.BiomeWeather biomeWeather = null;
                BiomeDefinition.BiomeType?  biomeType    = null;
                int stormLevel = 0;

                // Get biome the player is standing in
                BiomeDefinition biomeStandingOn = localPlayer.biomeStandingOn;
                if (biomeStandingOn != null)
                {
                    biomeType    = biomeStandingOn.m_BiomeType;
                    biomeWeather = WeatherManager.Instance.FindBiomeWeather(biomeType.Value);
                }

                // stormLevel > 0 means a storm is active
                WeatherManager.BiomeWeather currentWeather = WeatherManager.currentWeather;
                if (currentWeather?.biomeDefinition?.currentWeatherGroup != null)
                    stormLevel = currentWeather.biomeDefinition.currentWeatherGroup.stormLevel;

                // weatherSpectrum — e.g. "None", "Biome", "Snowy", "Stormy", "Rainy", "Foggy", "BloodMoon"
                weatherType = "None";
                if (biomeType != null && biomeWeather?.biomeDefinition != null)
                    weatherType = biomeWeather.biomeDefinition.weatherSpectrum.ToString();

                // Storm timer and fill
                if (biomeWeather != null && stormLevel > 0)
                {
                    int remaining = biomeWeather.stormWorldTime + biomeWeather.stormDuration - currentWorldTime;
                    if (remaining < 0) remaining = 0;

                    // Real-time seconds remaining — confirmed formula from CATUI
                    int timeOfDayIncPerSec = GameStats.GetInt(EnumGameStats.TimeOfDayIncPerSec);
                    int realSeconds = (timeOfDayIncPerSec > 0)
                        ? remaining / timeOfDayIncPerSec
                        : 0;

                    // Format as MM:SS using the same helper CATUI uses
                    stormTimer = XUiM_PlayerBuffs.ConvertToTimeString((float)realSeconds);

                    // Fill ratio for the storm arc (1.0 = full, 0.0 = expired)
                    float stormFill = (biomeWeather.stormDuration > 0)
                        ? (float)remaining / (float)biomeWeather.stormDuration
                        : 0f;
                    stormFill = Mathf.Clamp01(stormFill);

                    localPlayer.SetCVar("Ficus_stormFill",   stormFill);
                    localPlayer.SetCVar("Ficus_stormActive", 1f);
                }
                else
                {
                    stormTimer = "";
                    localPlayer.SetCVar("Ficus_stormFill",   0f);
                    localPlayer.SetCVar("Ficus_stormActive", 0f);
                }
            }
            catch (Exception e)
            {
                // Storm data can be unavailable early in load — silent fallback
                stormTimer = "";
                localPlayer.SetCVar("Ficus_stormFill",   0f);
                localPlayer.SetCVar("Ficus_stormActive", 0f);
                Debug.LogWarning("[FicusHUD] UpdateWeather: " + e.Message);
            }
        }

        private void SetCVars()
        {
            try
            {
                // Armor / speed / stages — confirmed working
                if (int.TryParse(armorRating, out int arm))
                    localPlayer.SetCVar("Ficus_armorRating", (float)arm);

                localPlayer.SetCVar("Ficus_playerSpeed", localPlayer.GetSpeedModifier());

                if (int.TryParse(gameStage, out int gs))
                {
                    localPlayer.SetCVar("Ficus_playerGameStage", (float)gs);
                    localPlayer.SetCVar("Ficus_playerLootStage",  (float)gs);
                }

                // XP fill — STUB 0.0f until dnSpy confirms Progression.Exp API
                // TODO: research EntityPlayerProgressionSDCS in Assembly-CSharp.dll
                //       Find Exp field (current total XP) and GetExpForLevel(int)
                //       Formula: (Exp - ExpForLevel(Level)) /
                //                (ExpForLevel(Level+1) - ExpForLevel(Level))
                localPlayer.SetCVar("Ficus_xpFill", 0.0f);

                // Storm CVars are set inside UpdateWeather()
            }
            catch (Exception e)
            {
                Debug.LogWarning("[FicusHUD] XUiC_InfoPanel.SetCVars: " + e.Message);
            }
        }

        // ── binding provider ─────────────────────────────────────────────────

        public override bool GetBindingValueInternal(ref string value, string bindingName)
        {
            switch (bindingName)
            {
                // Core stats
                case "ficus_worldday":    value = worldDay;    return true;
                case "ficus_worldhour":   value = worldHour;   return true;
                case "ficus_playername":  value = playerName;  return true;
                case "ficus_playerlevel": value = playerLevel; return true;
                case "ficus_armorrating": value = armorRating; return true;
                case "ficus_playerspeed": value = playerSpeed; return true;
                case "ficus_gamestage":   value = gameStage;   return true;
                case "ficus_lootstage":   value = lootStage;   return true;
                case "ficus_skillpoints": value = skillPoints; return true;

                // Time / weather
                case "ficus_isDaytime":   value = isDaytime;   return true;
                case "ficus_stormTimer":  value = stormTimer;  return true;
                case "ficus_weatherType": value = weatherType; return true;

                // Active item stubs
                case "ficus_activeItemName":     value = activeItemName;     return true;
                case "ficus_activeItemDurColor": value = activeItemDurColor; return true;

                // Notch pip colors
                case "ficus_notch1_color": value = notchColor[1]; return true;
                case "ficus_notch2_color": value = notchColor[2]; return true;
                case "ficus_notch3_color": value = notchColor[3]; return true;
                case "ficus_notch4_color": value = notchColor[4]; return true;
                case "ficus_notch5_color": value = notchColor[5]; return true;
                case "ficus_notch6_color": value = notchColor[6]; return true;
                case "ficus_notch7_color": value = notchColor[7]; return true;

                // Notch label colors
                case "ficus_notch1_lcolor": value = notchLColor[1]; return true;
                case "ficus_notch2_lcolor": value = notchLColor[2]; return true;
                case "ficus_notch3_lcolor": value = notchLColor[3]; return true;
                case "ficus_notch4_lcolor": value = notchLColor[4]; return true;
                case "ficus_notch5_lcolor": value = notchLColor[5]; return true;
                case "ficus_notch6_lcolor": value = notchLColor[6]; return true;
                case "ficus_notch7_lcolor": value = notchLColor[7]; return true;
            }

            return base.GetBindingValueInternal(ref value, bindingName);
        }
    }
}