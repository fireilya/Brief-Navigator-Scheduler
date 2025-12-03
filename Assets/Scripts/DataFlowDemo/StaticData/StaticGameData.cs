using System.Collections.Generic;

namespace DataFlowDemo.StaticData
{
    public static class StaticGameData
    {
        public static List<Task> Tasks = new()
        {
            new Task("Собрать 120 картошки", 120, new[]
            {
                new SubTask("Выкопать картошку", new Tool("Лопата")),
                new SubTask("Почистить картошку", new Tool("Кисть")),
                new SubTask("Собрать картошку", new Tool("Сумка"))
            }),

            new Task("Собрать 50 морковки", 50, new[]
            {
                new SubTask("Выкопать морковку", new Tool("Лопата")),
                new SubTask("Обрезать хвостики", new Tool("Ножницы")),
                new SubTask("Собрать морковку", new Tool("Сумка"))
            }),

            new Task("Собрать 20 тыкв", 20, new[]
            {
                new SubTask("Срезать тыквы", new Tool("Нож")),
                new SubTask("Обрезать хвостики", new Tool("Ножницы")),
                new SubTask("Собрать тыквы", new Tool("Садовая тачка"))
            })
        };

        public static List<Tool> Tools = new()
        {
            new Tool("Лопата"),
            new Tool("Кисть"),
            new Tool("Сумка"),
            new Tool("Нож"),
            new Tool("Ножницы"),
            new Tool("Садовая тачка")
        };
    }
}