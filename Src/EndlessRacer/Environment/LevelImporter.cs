﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Models;

namespace EndlessRacer.Environment
{
    internal static class LevelImporter
    {
        private const string BasePath = "Maps/";
        private const string BackgroundExtension = "_bg";
        private const string CollisionExtension = "_collision";

        private static List<Tuple<string, CrossingPoint, CrossingPoint>> _levelAssets = new()
        {
            new Tuple<string, CrossingPoint, CrossingPoint>("cc_01", CrossingPoint.Center, CrossingPoint.Center),
        };

        public static Dictionary<CrossingPoint, List<LevelSegmentTemplate>> Import(ContentManager content)
        {
            var templates = new Dictionary<CrossingPoint, List<LevelSegmentTemplate>>();

            foreach (var asset in _levelAssets)
            {
                var (name, entryPoint, exitPoint) = asset;

                var sprite = content.Load<Texture2D>($"{BasePath}{name}{BackgroundExtension}");
                var collisionData = content.Load<CollisionData>($"{BasePath}{name}{CollisionExtension}");

                var template = new LevelSegmentTemplate(entryPoint, exitPoint, sprite, collisionData.ToBoolMultiDimArray());

                if (!templates.ContainsKey(entryPoint))
                {
                    templates[entryPoint] = new List<LevelSegmentTemplate>();
                }

                templates[entryPoint].Add(template);
            }

            return templates;
        }
    }
}