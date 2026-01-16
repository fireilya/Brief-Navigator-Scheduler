using System;
using System.Collections.Generic;
using System.Linq;
using Domain.FlattenDtos;
using Domain.Scheduler;
using Scheduler.Data;
using UnityEngine;

namespace Shared
{
    public static class DBServerMock
    {
        private static ActionArea[] _actionAreas;
        private static Tool[] _tools;
        private static Neutralizer[] _neutralizers;
        private static Worker[] _workers;

        private static Guid GetToolIdByName(string toolName)
            => _tools.Single(x => string.Equals(
                x.Name,
                toolName,
                StringComparison.CurrentCultureIgnoreCase)).Id;

        private static Guid GetNeutralizerIdByName(string neutralizerName)
            => _neutralizers.Single(x => string.Equals(
                x.Name,
                neutralizerName,
                StringComparison.CurrentCultureIgnoreCase)).Id;

        public static void Init()
        {
            _tools = new[]
            {
                new Tool(Guid.NewGuid(), DataContainer.QuestId, "Лопата", "Tools/Shovel"),
                new Tool(Guid.NewGuid(), DataContainer.QuestId, "Нож", "Tools/Knife"),
                new Tool(Guid.NewGuid(), DataContainer.QuestId, "Серп", "Tools/Sickle"),
                new Tool(Guid.NewGuid(), DataContainer.QuestId, "Ведро", "Tools/Bucket"),
                new Tool(Guid.NewGuid(), DataContainer.QuestId, "Мешок", "Tools/Bag"),
                new Tool(Guid.NewGuid(), DataContainer.QuestId, "Ящик", "Tools/Box"),
                new Tool(Guid.NewGuid(), DataContainer.QuestId, "Тачка", "Tools/Wheelbarrow"),
            };

            _neutralizers = new[]
            {
                new Neutralizer(Guid.NewGuid(), "Ботинки", "Equipment/Boots"),
                new Neutralizer(Guid.NewGuid(), "Плащ", "Equipment/Cape"),
                new Neutralizer(Guid.NewGuid(), "Шляпа", "Equipment/Hat")
            };

            _workers = new[]
            {
                new Worker(Guid.NewGuid(), "Фермер", "Workers/Farmer", 1.0f),
                new Worker(Guid.NewGuid(), "Сын фермера", "Workers/FarmerSon", 0.8f)
            };

            _actionAreas = new[]
            {
                new ActionArea(
                    Guid.NewGuid(),
                    "ActionAreas/FarmActionArea",
                    "Ферма",
                    new[]
                    {
                        new Location(
                            Guid.NewGuid(),
                            "Поле",
                            "Locations/Fields",
                            new[]
                            {
                                new GameTask(
                                    Guid.NewGuid(),
                                    "Собрать кукурузу",
                                    90,
                                    4,
                                    new Subtask[]
                                    {
                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Срывать кукурузу",
                                            20,
                                            1,
                                            false,
                                            Guid.Empty),

                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Обрывать листья у початков",
                                            30,
                                            2,
                                            false,
                                            Guid.Empty),
                                        new CapacitySubtask(
                                            Guid.NewGuid(),
                                            "Собирать",
                                            10,
                                            3,
                                            true,
                                            5,
                                            new Dictionary<Guid, short>
                                            {
                                                { GetToolIdByName("Ведро"), 30 },
                                                { GetToolIdByName("Мешок"), 70 },
                                                { GetToolIdByName("Ящик"), 20 },
                                                { GetToolIdByName("Тачка"), 100 }
                                            })
                                    },
                                    true),

                                new GameTask(
                                    Guid.NewGuid(),
                                    "Собрать Сафлор",
                                    30,
                                    4,
                                    new Subtask[]
                                    {
                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Срывать сафлор",
                                            20,
                                            1,
                                            false,
                                            Guid.Empty),
                                        new CapacitySubtask(
                                            Guid.NewGuid(),
                                            "Собрать сафлор",
                                            10,
                                            2,
                                            true,
                                            30,
                                            new Dictionary<Guid, short>
                                            {
                                                { GetToolIdByName("Ведро"), 200 },
                                                { GetToolIdByName("Мешок"), 300 },
                                                { GetToolIdByName("Ящик"), 300 },
                                                { GetToolIdByName("Тачка"), 500 }
                                            }
                                        )
                                    },
                                    true),

                                new GameTask(
                                    Guid.NewGuid(),
                                    "Собрать Вербену",
                                    30,
                                    4,
                                    new Subtask[]
                                    {
                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Срезать вербену",
                                            30,
                                            1,
                                            false,
                                            GetToolIdByName("Серп")),
                                        new CapacitySubtask(
                                            Guid.NewGuid(),
                                            "Собрать Вербену",
                                            10,
                                            2,
                                            true,
                                            30,
                                            new Dictionary<Guid, short>
                                            {
                                                { GetToolIdByName("Ведро"), 300 },
                                                { GetToolIdByName("Мешок"), 500 },
                                                { GetToolIdByName("Ящик"), 300 },
                                                { GetToolIdByName("Тачка"), 500 }
                                            }
                                        )
                                    },
                                    false)
                            },
                            new Risk(
                                Guid.NewGuid(),
                                "Дождь",
                                "Risks/Rain",
                                "Дождь мокрый и холодный",
                                "Начался сильный ливень",
                                "Работник промок до нитки.",
                                GetNeutralizerIdByName("Плащ")),
                            0),
                        new Location(
                            Guid.NewGuid(),
                            "Огород",
                            "Locations/Garden",
                            new[]
                            {
                                new GameTask(
                                    Guid.NewGuid(),
                                    "Собрать картошку",
                                    120,
                                    1,
                                    new Subtask[]
                                    {
                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Выкопать картошку",
                                            30,
                                            1,
                                            false,
                                            GetToolIdByName("Лопата")),

                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Счистить землю",
                                            30,
                                            2,
                                            false,
                                            Guid.Empty),

                                        new CapacitySubtask(
                                            Guid.NewGuid(),
                                            "Собирать картошку",
                                            10,
                                            3,
                                            true,
                                            10,
                                            new Dictionary<Guid, short>
                                            {
                                                { GetToolIdByName("Ведро"), 60 },
                                                { GetToolIdByName("Мешок"), 100 },
                                                { GetToolIdByName("Ящик"), 30 },
                                                { GetToolIdByName("Тачка"), 100 }
                                            })
                                    },
                                    true),

                                new GameTask(
                                    Guid.NewGuid(),
                                    "Собрать тыкву",
                                    120,
                                    4,
                                    new Subtask[]
                                    {
                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Срезать тыкву",
                                            30,
                                            1,
                                            false,
                                            GetToolIdByName("Нож")),

                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Оборвать хвостики",
                                            10,
                                            2,
                                            false,
                                            Guid.Empty),

                                        new CapacitySubtask(
                                            Guid.NewGuid(),
                                            "Собрать тыквы",
                                            10,
                                            3,
                                            true,
                                            1,
                                            new Dictionary<Guid, short>
                                            {
                                                { GetToolIdByName("Ведро"), 5 },
                                                { GetToolIdByName("Мешок"), 20 },
                                                { GetToolIdByName("Ящик"), 10 },
                                                { GetToolIdByName("Тачка"), 30 }
                                            }
                                        )
                                    },
                                    true),

                                new GameTask(
                                    Guid.NewGuid(),
                                    "Собрать Пшеницу",
                                    20,
                                    3,
                                    new Subtask[]
                                    {
                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Срезать пшеницу",
                                            30,
                                            1,
                                            false,
                                            GetToolIdByName("Нож")),
                                        new CapacitySubtask(
                                            Guid.NewGuid(),
                                            "Собрать пшеницу",
                                            10,
                                            2,
                                            true,
                                            30,
                                            new Dictionary<Guid, short>
                                            {
                                                { GetToolIdByName("Ведро"), 200 },
                                                { GetToolIdByName("Мешок"), 500 },
                                                { GetToolIdByName("Ящик"), 200 },
                                                { GetToolIdByName("Тачка"), 500 }
                                            }
                                        )
                                    },
                                    false),

                                new GameTask(
                                    Guid.NewGuid(),
                                    "Собрать Петрушку",
                                    20,
                                    3,
                                    new Subtask[]
                                    {
                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Срывать петрушку",
                                            20,
                                            1,
                                            false,
                                            Guid.Empty),
                                        new CapacitySubtask(
                                            Guid.NewGuid(),
                                            "Собрать петрушку",
                                            10,
                                            2,
                                            true,
                                            30,
                                            new Dictionary<Guid, short>
                                            {
                                                { GetToolIdByName("Ведро"), 200 },
                                                { GetToolIdByName("Мешок"), 500 },
                                                { GetToolIdByName("Ящик"), 200 },
                                                { GetToolIdByName("Тачка"), 500 }
                                            }
                                        )
                                    },
                                    false)
                            },
                            new Risk(
                                Guid.NewGuid(),
                                "Гравий",
                                "Risks/Gravel",
                                "Гравий довольно острый",
                                "Ваш работник наступил на острый камень",
                                "Работник слегка повредил ногу.",
                                GetNeutralizerIdByName("Ботинки")),
                            1),
                        new Location(
                            Guid.NewGuid(),
                            "Теплицы",
                            "Locations/Greenhouse",
                            new[]
                            {
                                new GameTask(
                                    Guid.NewGuid(),
                                    "Собрать томаты",
                                    70,
                                    4,
                                    new Subtask[]
                                    {
                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Срезать томаты",
                                            30,
                                            1,
                                            false,
                                            GetToolIdByName("Нож")),

                                        new CapacitySubtask(
                                            Guid.NewGuid(),
                                            "Собрать томаты",
                                            10,
                                            2,
                                            true,
                                            10,
                                            new Dictionary<Guid, short>
                                            {
                                                { GetToolIdByName("Ведро"), 50 },
                                                { GetToolIdByName("Мешок"), 100 },
                                                { GetToolIdByName("Ящик"), 70 },
                                                { GetToolIdByName("Тачка"), 200 }
                                            })
                                    },
                                    true),

                                new GameTask(
                                    Guid.NewGuid(),
                                    "Собрать Укроп",
                                    20,
                                    3,
                                    new Subtask[]
                                    {
                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Срывать укроп",
                                            20,
                                            1,
                                            false,
                                            Guid.Empty),

                                        new CapacitySubtask(
                                            Guid.NewGuid(),
                                            "Собрать укроп",
                                            10,
                                            2,
                                            true,
                                            30,
                                            new Dictionary<Guid, short>
                                            {
                                                { GetToolIdByName("Ведро"), 200 },
                                                { GetToolIdByName("Мешок"), 500 },
                                                { GetToolIdByName("Ящик"), 200 },
                                                { GetToolIdByName("Тачка"), 500 }
                                            }
                                        )
                                    },
                                    false),

                                new GameTask(
                                    Guid.NewGuid(),
                                    "Собрать Салат",
                                    20,
                                    3,
                                    new Subtask[]
                                    {
                                        new ProcessSubtask(
                                            Guid.NewGuid(),
                                            "Срывать салат",
                                            20,
                                            1,
                                            false,
                                            Guid.Empty),

                                        new CapacitySubtask(
                                            Guid.NewGuid(),
                                            "Собрать салат",
                                            10,
                                            2,
                                            true,
                                            30,
                                            new Dictionary<Guid, short>
                                            {
                                                { GetToolIdByName("Ведро"), 200 },
                                                { GetToolIdByName("Мешок"), 500 },
                                                { GetToolIdByName("Ящик"), 200 },
                                                { GetToolIdByName("Тачка"), 500 }
                                            }
                                        )
                                    },
                                    false)
                            },
                            new Risk(
                                Guid.NewGuid(),
                                "Солнце",
                                "Risks/Sun",
                                "Солнце очень горячее!",
                                "Облака разошлись и на небе выглянуло жаркое солнце",
                                "Работник получил солнечный удар.",
                                GetNeutralizerIdByName("Шляпа")),
                            2),
                    })
            };

            IsInited = true;
        }

        public static bool IsInited { get; private set; } = false;

        public static ActionArea GetActionArea(Guid actionAreaId) =>
            _actionAreas.Single(x => x.Id == actionAreaId);

        public static ActionArea GetFirstActionArea() => _actionAreas.First();

        public static Tool GetTool(Guid toolId) =>
            _tools.Single(x => x.Id == toolId);

        public static Worker[] GetAllWorkers() => _workers;

        public static Guid[] GetAllCapacityToolId => new[]
        {
            GetToolIdByName("Ведро"),
            GetToolIdByName("Мешок"),
            GetToolIdByName("Ящик"),
            GetToolIdByName("Тачка")
        };

        public static Neutralizer GetNeutralizer(Guid neutralizerId) =>
            _neutralizers.FirstOrDefault(x => x.Id == neutralizerId);


        public static Tool[] GetAllToolsForQuest(Guid questId) => _tools.Where(x => x.QuestId == questId).ToArray();
    }
}