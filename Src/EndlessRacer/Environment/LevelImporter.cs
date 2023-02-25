using System;
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
            // starts in center
            new Tuple<string, CrossingPoint, CrossingPoint>("CC_01", CrossingPoint.Center, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("CC_02", CrossingPoint.Center, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("CL_01", CrossingPoint.Center, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("CR_01", CrossingPoint.Center, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("CR_02", CrossingPoint.Center, CrossingPoint.Right),

            // starts on left
            new Tuple<string, CrossingPoint, CrossingPoint>("LL_01", CrossingPoint.Left, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("LC_01", CrossingPoint.Left, CrossingPoint.Center),

            // starts on left
            new Tuple<string, CrossingPoint, CrossingPoint>("RR_01", CrossingPoint.Right, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("RC_01", CrossingPoint.Right, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("RC_02", CrossingPoint.Right, CrossingPoint.Center),

            // special
            new Tuple<string, CrossingPoint, CrossingPoint>("FinishLine", CrossingPoint.Center, CrossingPoint.Center),
        };

        public static Dictionary<CrossingPoint, List<LevelSegmentTemplate>> Import(ContentManager content)
        {
            var templates = new Dictionary<CrossingPoint, List<LevelSegmentTemplate>>();

            foreach (var asset in _levelAssets)
            {
                var (name, entryPoint, exitPoint) = asset;

                var sprite = content.Load<Texture2D>($"{BasePath}{name}{BackgroundExtension}");
                var collisionData = content.Load<CollisionData>($"{BasePath}{name}{CollisionExtension}");

                var template = new LevelSegmentTemplate(entryPoint, exitPoint, sprite, collisionData.ToMultiDimArray());

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
