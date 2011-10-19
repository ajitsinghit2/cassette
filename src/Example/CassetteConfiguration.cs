﻿#region License
/*
Copyright 2011 Andrew Davey

This file is part of Cassette.

Cassette is free software: you can redistribute it and/or modify it under the 
terms of the GNU General Public License as published by the Free Software 
Foundation, either version 3 of the License, or (at your option) any later 
version.

Cassette is distributed in the hope that it will be useful, but WITHOUT ANY 
WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS 
FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with 
Cassette. If not, see http://www.gnu.org/licenses/.
*/
#endregion

using Cassette.Configuration;
using Cassette.HtmlTemplates;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Example
{
    public class CassetteConfiguration : ICassetteConfiguration
    {
        public void Configure(IConfigurableCassetteApplication application)
        {
            // TODO: Perhaps having BundleCollection as parameter would be neater than typing application.Bundles each time!
            //       Then have the application.Settings as another parameter.

            application.Bundles.AddForEachSubDirectory<ScriptBundle>("Scripts");
            application.Bundles.Add(new ExternalScriptBundle("twitter", "http://platform.twitter.com/widgets.js")
            {
                Location = "body"
            });

            application.Bundles.Add(new StylesheetBundle("Styles")
            {
                Processor = new StylesheetPipeline // Replace the default processor with a customized pipeline.
                {
                    ConvertImageUrlsToDataUris = true // This property is false by default.
                }
            });

            application.Bundles.AddForEachSubDirectory<HtmlTemplateBundle>(
                "HtmlTemplates",
                bundle => bundle.Processor = new KnockoutJQueryTmplPipeline()
            );
        }
    }
}