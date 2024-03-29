// Owners: Nikolay Karlov, Grigorenko Paul
// This code is generated by a template, so don't modify it.
// All changes will be discarded.
// Last generated: 04/07/2022 21:12:35
// CacheTags.cs

using QA.DotNetCore.Caching.Interfaces;
using QA.DotNetCore.Engine.Persistent.Interfaces.Settings;
using System.Linq;

namespace Demosite.Templates
{
    /// <summary>
    /// Контентные версионные теги
    /// </summary>
	public static partial class CacheTags
    {
        /// <summary>
        /// AbstractItem
        /// Элемент структуры сайта
        /// </summary>
        public static QPContent QPAbstractItem = new() { Id = 537, SiteId = 52, Name = "AbstractItem", NetName = "QPAbstractItem" };
        /// <summary>
        /// ItemDefinition
        /// Типы страниц и виджетов
        /// </summary>
        public static QPContent QPDiscriminator = new() { Id = 538, SiteId = 52, Name = "ItemDefinition", NetName = "QPDiscriminator" };
        /// <summary>
        /// Локализация
        /// 
        /// </summary>
        public static QPContent QPCulture = new() { Id = 540, SiteId = 52, Name = "Локализация", NetName = "QPCulture" };
        /// <summary>
        /// ItemDefinitionConstraint
        /// 
        /// </summary>
        public static QPContent QPItemDefinitionConstraint = new() { Id = 10609, SiteId = 52, Name = "ItemDefinitionConstraint", NetName = "QPItemDefinitionConstraint" };
        /// <summary>
        /// AB тесты
        /// Контент для регистрации AB-тестов
        /// </summary>
        public static QPContent AbTest = new() { Id = 10663, SiteId = 52, Name = "AB тесты", NetName = "AbTest" };
        /// <summary>
        /// Контейнер AB тестирования
        /// 
        /// </summary>
        public static QPContent AbTestBaseContainer = new() { Id = 10664, SiteId = 52, Name = "Контейнер AB тестирования", NetName = "AbTestBaseContainer" };
        /// <summary>
        /// Скрипт AB тестирования
        /// 
        /// </summary>
        public static QPContent AbTestScript = new() { Id = 10665, SiteId = 52, Name = "Скрипт AB тестирования", NetName = "AbTestScript" };
        /// <summary>
        /// Контейнер скриптов
        /// 
        /// </summary>
        public static QPContent AbTestScriptContainer = new() { Id = 10666, SiteId = 52, Name = "Контейнер скриптов", NetName = "AbTestScriptContainer" };
        /// <summary>
        /// Контейнер клиентских редиректов
        /// 
        /// </summary>
        public static QPContent AbTestClientRedirectContainer = new() { Id = 10667, SiteId = 52, Name = "Контейнер клиентских редиректов", NetName = "AbTestClientRedirectContainer" };
        /// <summary>
        /// Клиентский редирект AB тестирования
        /// 
        /// </summary>
        public static QPContent AbTestClientRedirect = new() { Id = 10668, SiteId = 52, Name = "Клиентский редирект AB тестирования", NetName = "AbTestClientRedirect" };
        /// <summary>
        /// Блог Посты
        /// 
        /// </summary>
        public static QPContent BlogPost = new() { Id = 30745, SiteId = 52, Name = "Блог Посты", NetName = "BlogPost" };
        /// <summary>
        /// Блог Категории
        /// 
        /// </summary>
        public static QPContent BlogCategory = new() { Id = 30746, SiteId = 52, Name = "Блог Категории", NetName = "BlogCategory" };
        /// <summary>
        /// Блог Теги
        /// 
        /// </summary>
        public static QPContent BlogTag = new() { Id = 30747, SiteId = 52, Name = "Блог Теги", NetName = "BlogTag" };
        /// <summary>
        /// FAQ Вопросы и ответы
        /// 
        /// </summary>
        public static QPContent FaqItem = new() { Id = 30749, SiteId = 52, Name = "FAQ Вопросы и ответы", NetName = "FaqItem" };
        /// <summary>
        /// NewItem
        /// 
        /// </summary>
        public static QPContent NewItem = new() { Id = 30758, SiteId = 52, Name = "NewItem", NetName = "NewItem" };
    }

    /// <summary>
    /// Утилиты для работы с кештегами
    /// </summary>
    public class CacheTagUtilities
    {
        private readonly IQpContentCacheTagNamingProvider _qpContentCacheTagNamingProvider;
        private readonly QpSettings _qpSettings;

        public CacheTagUtilities(IQpContentCacheTagNamingProvider qpContentCacheTagNamingProvider, QpSettings qpSettings)
        {
            _qpContentCacheTagNamingProvider = qpContentCacheTagNamingProvider;
            _qpSettings = qpSettings;
        }

        /// <summary>
        /// Преобразование тегов в массив
        /// </summary>
        public string[] Merge(params QPContent[] tags)
        {
            return tags.Select(c => _qpContentCacheTagNamingProvider.Get(c.Name, c.SiteId, _qpSettings.IsStage)).Where(t => t != null).ToArray();
        }
    }

    public class QPContent
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
        public string NetName { get; set; }
    }
}
