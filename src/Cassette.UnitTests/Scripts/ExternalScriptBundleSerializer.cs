﻿using System.Xml.Linq;
using Moq;
using Should;
using Xunit;

namespace Cassette.Scripts
{
    public class ExternalScriptBundleSerializer_Tests
    {
        ExternalScriptBundle bundle;
        XElement element;

        public ExternalScriptBundleSerializer_Tests()
        {
            bundle = new ExternalScriptBundle("http://example.com/", "~", "condition")
            {
                Hash = new byte[0],
                FallbackRenderer = new ScriptBundleHtmlRenderer(Mock.Of<IUrlGenerator>()),
                Renderer = new ExternalScriptBundle.ExternalScriptBundleRenderer(new CassetteSettings())
            };

            SerializeToElement();
        }

        [Fact]
        public void UrlAttributeEqualsManifestExternalUrl()
        {
            element.Attribute("Url").Value.ShouldEqual(bundle.ExternalUrl);
        }

        [Fact]
        public void FallbackConditionAttributeEqualsManifestFallbackCondition()
        {
            element.Attribute("FallbackCondition").Value.ShouldEqual(bundle.FallbackCondition);
        }

        [Fact]
        public void GivenManifestFallbackConditionIsNullThenElementHasNoFallbackConditionAttribute()
        {
            bundle = new ExternalScriptBundle("http://example.com/")
            {
                Hash = new byte[0],
                FallbackRenderer = new ScriptBundleHtmlRenderer(Mock.Of<IUrlGenerator>()),
                Renderer = new ExternalScriptBundle.ExternalScriptBundleRenderer(new CassetteSettings())
            };
            SerializeToElement();
            element.Attribute("FallbackCondition").ShouldBeNull();
        }

        void SerializeToElement()
        {
            var container = new XDocument();
            var writer = new ExternalScriptBundleSerializer(container);
            writer.Serialize(bundle);
            element = container.Root;
        }
    }
}