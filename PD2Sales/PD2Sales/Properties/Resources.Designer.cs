﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PD2Sales.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PD2Sales.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to name	alias1	alias2	readableName	shortenedName
        ///ac			Armor	Arm
        ///ac-miss			Defense VS Missiles	MissileArmor
        ///ac-hth			ac-hth	ac-hth
        ///red-dmg			Reduced Damage Taken	DR
        ///red-dmg%			% Reduced Damage Taken	%DR
        ///ac%			% Enhanced Defense	%EDef
        ///red-mag			Reduced Magic Damage Taken	RMD
        ///str			Strength	Str
        ///dex			Dexterity	Dex
        ///vit			Vitality	Vit
        ///enr			Energy	Nrg
        ///mana			Mana	Mana
        ///mana%			%Mana	%Mana
        ///hp			HP	HP
        ///hp%			%HP	%HP
        ///att			Attack Damage	Atk
        ///block			Block	Block
        ///cold-min			Mininum Cold Damage	ColdMin
        ///co [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string PD2Stat_Names {
            get {
                return ResourceManager.GetString("PD2Stat_Names", resourceCulture);
            }
        }
    }
}
