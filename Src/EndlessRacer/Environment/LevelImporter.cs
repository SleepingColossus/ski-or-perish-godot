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

        private static readonly string[][] CareerLevels = new[]
        {
            /* Level 01 */ new[] { "CC_02", "CR_02", "RC_02", "FinishLine", "CC_02" },
            /* Level 02 */ new[] { "CC_02", "CR_02", "RC_02", "FinishLine", "CC_02" },
            /* Level 03 */ new[] { "CC_02", "CR_02", "RC_02", "FinishLine", "CC_02" },
            /* Level 04 */ new[] { "CC_02", "CR_02", "RC_02", "FinishLine", "CC_02" },
            /* Level 05 */ new[] { "CC_02", "CR_02", "RC_02", "FinishLine", "CC_02" },
            /* Level 06 */ new[] { "CC_02", "CR_02", "RC_02", "FinishLine", "CC_02" },
            /* Level 07 */ new[] { "CC_02", "CR_02", "RC_02", "FinishLine", "CC_02" },
            /* Level 08 */ new[] { "CC_02", "CR_02", "RC_02", "FinishLine", "CC_02" },
            /* Level 09 */ new[] { "CC_02", "CR_02", "RC_02", "FinishLine", "CC_02" },
            /* Level 10 */ new[] { "CC_02", "CR_02", "RC_02", "FinishLine", "CC_02" },
        };

        private static readonly List<Tuple<string, CrossingPoint, CrossingPoint>> LevelAssets = new()
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

        public static List<LevelSegmentTemplate> ImportCareerLevel(ContentManager content, int levelNumber)
        {
            return ImportLevel(content, levelNumber);
        }

        // used by predefined levels
        private static List<LevelSegmentTemplate> ImportLevel(ContentManager content, int levelNumber)
        {
            var level = CareerLevels[levelNumber];

            static Dictionary<string, LevelSegmentTemplate> ImportByName(ContentManager content)
            {
                var templates = new Dictionary<string, LevelSegmentTemplate>();

                foreach (var asset in LevelAssets)
                {
                    var (name, entryPoint, exitPoint) = asset;

                    var sprite = content.Load<Texture2D>($"{BasePath}{name}{BackgroundExtension}");
                    var collisionData = content.Load<CollisionData>($"{BasePath}{name}{CollisionExtension}");

                    var template = new LevelSegmentTemplate(entryPoint, exitPoint, sprite, collisionData.ToMultiDimArray());

                    templates.Add(name, template);
                }

                return templates;
            }

            var templatesByName = ImportByName(content);

            var templates = new List<LevelSegmentTemplate>();

            foreach (var name in level)
            {
                templates.Add(templatesByName[name]);
            }

            return templates;
        }

        // used by endless level
        public static Dictionary<CrossingPoint, List<LevelSegmentTemplate>> ImportByEntryPoint(ContentManager content)
        {
            var templates = new Dictionary<CrossingPoint, List<LevelSegmentTemplate>>();

            foreach (var asset in LevelAssets)
            {
                var (name, entryPoint, exitPoint) = asset;

                // skip finish line in endless
                if (name == "FinishLine")
                {
                    continue;
                }

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
