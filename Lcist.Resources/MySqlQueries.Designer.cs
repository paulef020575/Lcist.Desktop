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
    public class MySqlQueries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MySqlQueries() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Lcist.Resources.MySqlQueries", typeof(MySqlQueries).Assembly);
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
        ///   Ищет локализованную строку, похожую на SELECT Id FROM days WHERE user=@user AND dateMark = @date.
        /// </summary>
        public static string CheckId {
            get {
                return ResourceManager.GetString("CheckId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT COUNT(*) FROM matches WHERE player = @idPlayer AND matchDate = @dateMatch.
        /// </summary>
        public static string CheckMatchByDate {
            get {
                return ResourceManager.GetString("CheckMatchByDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT COUNT(*) FROM rithmes WHERE Id = @Id.
        /// </summary>
        public static string CheckPersonalResultId {
            get {
                return ResourceManager.GetString("CheckPersonalResultId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT COUNT(*) FROM players WHERE Id = @id.
        /// </summary>
        public static string CheckPlayerById {
            get {
                return ResourceManager.GetString("CheckPlayerById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT COUNT(*) FROM queries WHERE Id = @id.
        /// </summary>
        public static string CheckQueryById {
            get {
                return ResourceManager.GetString("CheckQueryById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM rithmes WHERE stage1 = @stage.
        /// </summary>
        public static string GetCurrentRhythmQueries {
            get {
                return ResourceManager.GetString("GetCurrentRhythmQueries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на INSERT INTO days (user, dateMark, Mark1, Mark2) VALUES (@user, @date, @mark1, @mark2).
        /// </summary>
        public static string InsertDay {
            get {
                return ResourceManager.GetString("InsertDay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на INSERT INTO matches (user, player, matchDate, goal1, goal2, goal3, yellow1, red, reason1, reason3, personal, stage) VALUES (@idUser, @idPlayer, @dateMatch, @goal1, @goal2, @goal3, @yellow, @red, @reason1, @reason3, @personal, 2).
        /// </summary>
        public static string InsertMatch {
            get {
                return ResourceManager.GetString("InsertMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на INSERT INTO rithmes (id, user, dateFrom, length, date1, stage1, date2, stage2, date3, stage3) values (@id, @user, @dateFrom, @length, @date1, @stage, @date2, 0, @date3, 0).
        /// </summary>
        public static string InsertPersonalResult {
            get {
                return ResourceManager.GetString("InsertPersonalResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на INSERT INTO players (Id, Name, Birthday, user, state, cost, stage) VALUES (@id, @name, @birthday, @idUser, @state, @cost, 2).
        /// </summary>
        public static string InsertPlayer {
            get {
                return ResourceManager.GetString("InsertPlayer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на INSERT INTO queries (id, user, dateQuery, dateFor, stage, player, position, phisical, emotional, shift1, shift2, shift3) VALUES (@id, @idUser, @dateQuery, @dateFor, 2, @idPlayer, 0, @phisical, @emotional, @shift1, @shift2, @shift3).
        /// </summary>
        public static string InsertQuery {
            get {
                return ResourceManager.GetString("InsertQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM days WHERE user = @user ORDER BY dateMark.
        /// </summary>
        public static string LoadDays {
            get {
                return ResourceManager.GetString("LoadDays", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT u.Id, u.Name, u.RealName, 
        ///	(SELECT COUNT( d.Id ) FROM days d WHERE d.user = u.Id) AS DaysCount, 
        ///	(SELECT COUNT( p.Id ) FROM players p WHERE p.user = u.Id) AS PlayersCount
        ///FROM users u
        ///GROUP BY u.Id, u.Name, u.RealName.
        /// </summary>
        public static string LoadUsers {
            get {
                return ResourceManager.GetString("LoadUsers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на UPDATE players SET name = @name WHERE Id = @id.
        /// </summary>
        public static string UpdateName {
            get {
                return ResourceManager.GetString("UpdateName", resourceCulture);
            }
        }
    }
}
