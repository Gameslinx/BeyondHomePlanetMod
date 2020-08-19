

//As with KSP rules apparently I have to include my source code here
//Screw you Squad/T2, you don't even let us decompile your DLLs AND you have a history of potentially MALICIOUS code
//I can do whatever I damn well please. But, here's my source code :DDDDDDDD
//Lmao, it's too funny asking for transparency when you won't give it yourself: FOR[T2/Skwod]

using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Kopernicus;
using Kopernicus.Constants;

namespace BeyondHomeInstallationChecker
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class BICH : MonoBehaviour
    {
        public void Start()
        {
            int memorysize = (SystemInfo.systemMemorySize / 1000);
            int gpumemorysize = SystemInfo.graphicsMemorySize / 1000;
            int errorcount = 0;
            string errors = "";
            Debug.Log("----------------------------------------------------------------------------------");
            Debug.Log("");
            Debug.Log("");
            DoLogThingsForMe("Starting up! - Beyond Home by Gameslinx - ARR");   //Beyondhome Installation Checker. Calm down, calm down.
            DoLogThingsForMe("BICH stands for Beyondhome Installation Checker. If you're reading this, you're evaluating what went wrong or stumbled across it when something else went wrong.");
            DoLogThingsForMe("Thank you for using Beyond Home. Below you can find out what went wrong (or right!)");
            string path = Application.dataPath.Remove(Application.dataPath.Length - 12, 12) + "GameData/";
            DoLogThingsForMe("GameData directory is " + path);  //Well, I sure hope so
            Debug.Log("");
            DoLogThingsForMe("Displaying errors, warnings and notices below:");
            if (memorysize > 4.9)
            {
                errors = errors + ("<color=#00F200>Recommended RAM: 5GB  -  Installed RAM: " + memorysize.ToString() + "\n");
                DoLogThingsForMe("  - RAM meets or exceeds requirement");
            }
            else
            {
                errors = errors + ("<color=#EF0013>Recommended RAM: 5GB  -  Installed RAM: " + memorysize.ToString() + "\n");
                DoLogThingsForMe("  - RAM does not meet the requirement");
            }
            if (gpumemorysize > 1)
            {
                errors = errors + ("<color=#00F200>Recommended VRAM: 1GB  -  Installed VRAM: " + gpumemorysize.ToString() + "\n");
                DoLogThingsForMe("  - VRAM meets or exceeds the requirement");
            }
            else
            {
                errors = errors + ("<color=#00F200>Recommended VRAM: 1GB  -  Installed VRAM: " + gpumemorysize.ToString() + "\n");
                DoLogThingsForMe("  - VRAM does not meet the requirement");
            }

            if (System.IO.File.Exists(path + "BeyondHome/BeyondHome_LICENSE.txt") == false)
            {
                errors = errors + "<color=#FF3F00>ERROR: Beyond Home is not installed!</color>\n";   //Nobody goes into the mod folder and removes the license file, so this indicates a bad install
                DoLogThingsForMe("  - Beyond Home is not even installed...?");
                errorcount++;
            }
            if (System.IO.File.Exists(path + "Kopernicus/Plugins/Kopernicus.dll") == false)
            {
                errors = errors + "<color=#FF3F00>ERROR: Kopernicus is not installed!</color>\n";
                DoLogThingsForMe("  - Kopernicus is not installed");
                errorcount++;
            }
            if (System.IO.File.Exists(path + "ModularFlightIntegrator/ModularFlightIntegrator.dll") == false)
            {
                errors = errors + "<color=#FF3F00>ERROR: Modular Flight Integrator is not installed!</color>\n";
                DoLogThingsForMe("  - MFI is not installed");
                errorcount++;
            }
//            if (System.IO.File.Exists(path + "Sigma/Replacements/SkyBox/Plugins/SigmaReplacementsSkyBox.dll") == true)
//            {
//                errors = errors + "<color=#FF7200>WARNING: Sigma Replacements: Skybox is not installed!</color>\n";   //SR:S is not on CKAN, so this is the only way to get the inferior people who actually use CKAN to install the damn dependencies
//                DoLogThingsForMe("  - SR:S is not installed");
//                errorcount++;
//            }
            if (System.IO.File.Exists(path + "AstronomersVisualPack/AstronomersVisualPack.version") == true)
            {
                errors = errors + "<color=#FF7200>WARNING: Astronomer's Visual Pack is not supported! Beyond Home adds its own visuals</color>\n";
                DoLogThingsForMe("  - AVP is installed");
                errorcount++;
            }
            if (System.IO.File.Exists(path + "StockVisualEnhancements/StockVisualEnhancements.version") == true)
            {
                errors = errors + "<color=#FF7200>WARNING: Stock Visual Enhancements is not supported! Beyond Home adds its own visuals</color>\n";
                DoLogThingsForMe("  - SVE is installed");
                errorcount++;
            }

            if (System.IO.File.Exists(path + "scatterer/scatterer.dll") == false)
            {
                errors = errors + "NOTICE: Scatterer is not installed!\n";
                DoLogThingsForMe("  - Scatterer is not installed");
                errorcount++;
            }
            if (System.IO.File.Exists(path + "EnvironmentalVisualEnhancements/License.txt") == false)
            {
                errors = errors + "NOTICE: Environmental Visual Enhancements is not installed!\n";
                DoLogThingsForMe("  - EVE is not installed");
                errorcount++;
            }
            if (GameSettings.PLANET_SCATTER == false)
            {
                errors += "NOTICE: Terrain Scatters are not enabled!\n";
                DoLogThingsForMe("  - Scatters not enabled");
                errorcount++;
            }
            if (GameSettings.PLANET_SCATTER_FACTOR < 1)
            {
                errors += "NOTICE: Terrain Scatter density must be 100%\n";
                DoLogThingsForMe("  - Scatters not 100% density");
                errorcount++;
            }
            if (errorcount == 0)
            {
                errors += "Beyond Home seems to be installed correctly! Thank you, and enjoy!\n\nAny issues? Contact me on the forums or discord (Gameslinx#0544)\n";
                DoLogThingsForMe("  - No file path errors detected!");
            }
            
            BeyondHomePostMessage(errors);
            Debug.Log("");
            DoLogThingsForMe("Checking for Kopernicus and KSP version match...");
            Debug.Log("");
            if (errorcount > 0)
            {
                DoLogThingsForMe("Exception: There were the following errors in the Beyond Home installation:");
                DoLogThingsForMe(errors);
            }
            try
            {
                bool validation = InstallCheckerGetKop();
                if (validation is false)
                {
                    ScreenMessages.PostScreenMessage("\n\n\n\n\n\n\n\n\n<color=#FF3F00>Your Kopernicus version does not match the KSP version!</color>");
                    DoLogThingsForMe("  - Kopernicus and KSP version mismatch!");
                }
            }
            catch (Exception e)
            {
                DoLogThingsForMe("Couldn't find either Kopernicus or the KSP readme.txt");
                Debug.Log(e.ToString());
                DoLogThingsForMe(e.ToString());
                ScreenMessages.PostScreenMessage("\n\n\n\n\n\n\n\n\nUnable to detect KSP / Kopernicus version");
            }
            DoLogThingsForMe("Shutting down InstallationChecker, its task is done! �Not all those who wander are lost...�");
            Debug.Log("");
            Debug.Log("");
            Debug.Log("----------------------------------------------------------------------------------");
        }
        public bool InstallCheckerGetKop()
        {
            string KSPpath = (Application.dataPath.Remove(Application.dataPath.Length - 12, 12) + "readme.txt");    //Locate KSP directory readme
            string KOPpath = (Application.dataPath.Remove(Application.dataPath.Length - 12, 12) + "GameData/Kopernicus/Plugins/Kopernicus.version");    //Locate kopernicus readme if it exists
            DoLogThingsForMe("Detected KSP validation path as " + KSPpath);
            DoLogThingsForMe("Detected Kopernicus validation path as " + KOPpath);
            bool x = CompatibilityChecker.IsCompatible();
            if (x is true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void BeyondHomePostMessage(string input)
        {
            ScreenMessages.PostScreenMessage(input);
        }
        public void DoLogThingsForMe(string input)
        {
            Debug.Log("[BICH] " + input);
        }
    }
}