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
        private const string ForegroundExtension = "_fg";
        private const string CollisionExtension = "_collision";

        public const string FinishLine = "FinishLine";

        private static readonly string[][] CareerLevels = new[]
        {
            /* Level 01 */ new[] { "CC_02", "CL_02", "RC_02", "FinishLine", "CC_02" },
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
            new Tuple<string, CrossingPoint, CrossingPoint>("CC_03", CrossingPoint.Center, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("CC_04", CrossingPoint.Center, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("CC_05", CrossingPoint.Center, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("CC_06", CrossingPoint.Center, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("CC_07", CrossingPoint.Center, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("CC_08", CrossingPoint.Center, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("CC_09", CrossingPoint.Center, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("CC_10", CrossingPoint.Center, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("CL_01", CrossingPoint.Center, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("CL_02", CrossingPoint.Center, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("CL_03", CrossingPoint.Center, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("CL_04", CrossingPoint.Center, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("CL_05", CrossingPoint.Center, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("CL_06", CrossingPoint.Center, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("CL_07", CrossingPoint.Center, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("CL_08", CrossingPoint.Center, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("CL_09", CrossingPoint.Center, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("CL_10", CrossingPoint.Center, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("CR_01", CrossingPoint.Center, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("CR_02", CrossingPoint.Center, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("CR_03", CrossingPoint.Center, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("CR_04", CrossingPoint.Center, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("CR_05", CrossingPoint.Center, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("CR_06", CrossingPoint.Center, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("CR_07", CrossingPoint.Center, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("CR_08", CrossingPoint.Center, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("CR_09", CrossingPoint.Center, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("CR_10", CrossingPoint.Center, CrossingPoint.Right),

            // starts on left
            new Tuple<string, CrossingPoint, CrossingPoint>("LC_01", CrossingPoint.Left, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("LC_02", CrossingPoint.Left, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("LC_03", CrossingPoint.Left, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("LC_04", CrossingPoint.Left, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("LC_05", CrossingPoint.Left, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("LC_06", CrossingPoint.Left, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("LC_07", CrossingPoint.Left, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("LC_08", CrossingPoint.Left, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("LC_09", CrossingPoint.Left, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("LC_10", CrossingPoint.Left, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("LL_01", CrossingPoint.Left, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("LL_02", CrossingPoint.Left, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("LL_03", CrossingPoint.Left, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("LL_04", CrossingPoint.Left, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("LL_05", CrossingPoint.Left, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("LL_06", CrossingPoint.Left, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("LL_07", CrossingPoint.Left, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("LL_08", CrossingPoint.Left, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("LL_09", CrossingPoint.Left, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("LL_10", CrossingPoint.Left, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("LR_01", CrossingPoint.Left, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("LR_02", CrossingPoint.Left, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("LR_03", CrossingPoint.Left, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("LR_04", CrossingPoint.Left, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("LR_05", CrossingPoint.Left, CrossingPoint.Right),

            // starts on right
            new Tuple<string, CrossingPoint, CrossingPoint>("RC_01", CrossingPoint.Right, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("RC_02", CrossingPoint.Right, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("RC_03", CrossingPoint.Right, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("RC_04", CrossingPoint.Right, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("RC_05", CrossingPoint.Right, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("RC_06", CrossingPoint.Right, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("RC_07", CrossingPoint.Right, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("RC_08", CrossingPoint.Right, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("RC_09", CrossingPoint.Right, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("RC_10", CrossingPoint.Right, CrossingPoint.Center),
            new Tuple<string, CrossingPoint, CrossingPoint>("RL_01", CrossingPoint.Right, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("RL_01", CrossingPoint.Right, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("RL_02", CrossingPoint.Right, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("RL_03", CrossingPoint.Right, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("RL_04", CrossingPoint.Right, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("RL_05", CrossingPoint.Right, CrossingPoint.Left),
            new Tuple<string, CrossingPoint, CrossingPoint>("RR_01", CrossingPoint.Right, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("RR_02", CrossingPoint.Right, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("RR_03", CrossingPoint.Right, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("RR_04", CrossingPoint.Right, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("RR_05", CrossingPoint.Right, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("RR_06", CrossingPoint.Right, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("RR_07", CrossingPoint.Right, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("RR_08", CrossingPoint.Right, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("RR_09", CrossingPoint.Right, CrossingPoint.Right),
            new Tuple<string, CrossingPoint, CrossingPoint>("RR_10", CrossingPoint.Right, CrossingPoint.Right),

            // special
            new Tuple<string, CrossingPoint, CrossingPoint>(FinishLine, CrossingPoint.Center, CrossingPoint.Center),
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

                    var sprite = content.Load<Texture2D>($"{BasePath}{name}");
                    var collisionData = content.Load<CollisionData>($"{BasePath}{name}{CollisionExtension}");

                    Texture2D foreground = null;

                    if (name == FinishLine)
                    {
                        foreground = content.Load<Texture2D>($"{BasePath}{name}{ForegroundExtension}");
                    }

                    var template = new LevelSegmentTemplate(entryPoint, exitPoint, sprite, collisionData.ToMultiDimArray(), foreground);

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
                if (name == FinishLine)
                {
                    continue;
                }

                var sprite = content.Load<Texture2D>($"{BasePath}{name}");
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
