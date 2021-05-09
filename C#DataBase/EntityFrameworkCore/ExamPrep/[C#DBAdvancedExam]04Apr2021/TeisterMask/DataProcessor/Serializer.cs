﻿namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {

            var result = context.Projects
                .ToArray()
                .Where(p => p.Tasks.Count > 0)
                .Select(p => new ExportProjectsXmlDto()
                {
                    TasksCount = p.Tasks.Count,
                    ProjectName = p.Name,
                    HasEndDate = p.DueDate == null ? "No" : "Yes",
                    Tasks = p.Tasks
                        .ToArray()
                        .OrderBy(p => p.Name)
                        .Select(t => new ExportTasksDto()
                        {
                            Name = t.Name,
                            Label = t.LabelType.ToString(),
                        })
                        .ToArray()
                })
                .OrderByDescending(p => p.TasksCount)
                .ThenBy(p => p.ProjectName)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportProjectsXmlDto[]), new XmlRootAttribute("Projects"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringWriter writer = new StringWriter(sb);

            using (writer)
            {
                serializer.Serialize(writer, result, namespaces);
            }

            return sb.ToString().Trim();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var result = context.Employees
                .ToList()
                .Where(e => e.EmployeesTasks.Any(et => et.Task.OpenDate >= date))
                .Select(e => new
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                            .ToArray()
                            .Where(t => t.Task.OpenDate >= date)
                            .OrderByDescending(t => t.Task.DueDate)
                            .ThenBy(t => t.Task.Name)
                            .Select(et => new
                            {
                                TaskName = et.Task.Name,
                                OpenDate = et.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                                DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                                LabelType = et.Task.LabelType.ToString(),
                                ExecutionType = et.Task.ExecutionType.ToString()
                            })
                            .ToArray()
                            
                })
                .OrderByDescending(e => e.Tasks.Count())
                .ThenBy(e => e.Username)
                .Take(10)
                .ToList();


            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }
    }
}