using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using UnityEngine;
using KSP.UI.Screens;

namespace ImpossibleInnovations
{
    [KSPAddon(KSPAddon.Startup.MainMenu , true)]
    public class II_Icons : MonoBehaviour
    {
        private void addIIfilter()
        {
            //Loading Textures
            Texture2D unselected = new Texture2D(32, 32);
            Texture2D selected = new Texture2D(32, 32);
            Texture2D unselectedLegacy = new Texture2D(32, 32);
            Texture2D selectedLegacy = new Texture2D(32, 32);

            unselected.LoadImage(File.ReadAllBytes("GameData/ImpossibleInnovations/Plugins/PluginData/SmallLogo.png"));
            selected.LoadImage(File.ReadAllBytes("GameData/ImpossibleInnovations/Plugins/PluginData/SmallLogoON.png"));
            RUI.Icons.Selectable.Icon filterIcon = new RUI.Icons.Selectable.Icon("II_filter_icon", unselected, selected); //Defining filterIcon

            unselectedLegacy.LoadImage(File.ReadAllBytes("GameData/ImpossibleInnovations/Plugins/PluginData/SmallLogoGrey.png"));
            selectedLegacy.LoadImage(File.ReadAllBytes("GameData/ImpossibleInnovations/Plugins/PluginData/SmallLogoGreyON.png"));
            RUI.Icons.Selectable.Icon filterIconLegacy = new RUI.Icons.Selectable.Icon("II_filter_icon_legacy", unselectedLegacy, selectedLegacy); //Defining filterIconLegacy

            PartCategorizer.Category IIfilter = PartCategorizer.AddCustomFilter("Impossible Innovations", filterIcon, Color.white);

            //filters for all II parts
            PartCategorizer.AddCustomSubcategoryFilter(IIfilter, "All Impossible Innovations Parts", filterIcon, o => o.manufacturer == "Impossible Innovations" && !o.title.Contains("(LEGACY)"));
            PartCategorizer.AddCustomSubcategoryFilter(IIfilter, "Tanks", filterIcon, p => p.resourceInfos.Exists(q => q.resourceName == "Deuterium" || q.resourceName == "Tritium") && p.manufacturer == "Impossible Innovations");
            PartCategorizer.AddCustomSubcategoryFilter(IIfilter, "Engines", filterIcon, r => r.title.Contains("Fusion Engine") && r.manufacturer == "Impossible Innovations");
            PartCategorizer.AddCustomSubcategoryFilter(IIfilter, "CL-20 Boosters", filterIcon, s => s.resourceInfos.Exists(t => t.resourceName == "CL-20") && s.manufacturer == "Impossible Innovations");
            PartCategorizer.AddCustomSubcategoryFilter(IIfilter, "Ionized Wings", filterIcon, u => u.title.Contains("Ionized") && !u.title.Contains("(LEGACY)") && u.manufacturer == "Impossible Innovations");
            PartCategorizer.AddCustomSubcategoryFilter(IIfilter, "Legacy Parts", filterIconLegacy, v => v.title.Contains("(LEGACY)") && v.manufacturer == "Impossible Innovations");
        }

        private void Awake()
        {
            GameEvents.onGUIEditorToolbarReady.Add(addIIfilter);
        }
    }
}
