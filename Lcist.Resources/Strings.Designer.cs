﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lcist.Resources {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Lcist.Resources.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на не выбран пользователь.
        /// </summary>
        public static string EmptyUser {
            get {
                return ResourceManager.GetString("EmptyUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Минимальное количество оценок по умолчанию .
        /// </summary>
        public static string PeriodCountSetting {
            get {
                return ResourceManager.GetString("PeriodCountSetting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Максимальная продолжительность периода по умолчанию (месяцев).
        /// </summary>
        public static string PeriodLengthSetting {
            get {
                return ResourceManager.GetString("PeriodLengthSetting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Персональный расчет биоритмов.
        /// </summary>
        public static string RhythmTitle {
            get {
                return ResourceManager.GetString("RhythmTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Настройки.
        /// </summary>
        public static string Settings {
            get {
                return ResourceManager.GetString("Settings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Центр управления ЛЦИСТ.
        /// </summary>
        public static string StartTitle {
            get {
                return ResourceManager.GetString("StartTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Восстановление данных игроков.
        /// </summary>
        public static string Upload {
            get {
                return ResourceManager.GetString("Upload", resourceCulture);
            }
        }
    }
}
