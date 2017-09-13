﻿using System;
using System.Reflection;
using System.Xml;
using System.IO;
using log4net;
using log4net.Config;
using log4net.Repository;
using Dark.Core.Extension;
using Castle.Core.Logging;

namespace Dark.Core.Log
{
    public class Log4NetLoggerFactory : AbstractLoggerFactory
    {
        internal const string DefaultConfigFileName = "log4net.config";
        private readonly ILoggerRepository _loggerRepository;

        public Log4NetLoggerFactory()
            : this(DefaultConfigFileName)
        {
        }

        public Log4NetLoggerFactory(string configFileName)
        {
            //Assembly.GetExecutingAssembly();
            _loggerRepository = LogManager.CreateRepository(
                typeof(Log4NetLoggerFactory).GetAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy)
            );

            var filePath = AppDomain.CurrentDomain.BaseDirectory + configFileName;

            var log4NetConfig = new XmlDocument();
            log4NetConfig.Load(File.OpenRead(filePath));
            XmlConfigurator.Configure(_loggerRepository, log4NetConfig["log4net"]);
        }

        public override ILogger Create(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }


            return new Log4NetLogger(LogManager.GetLogger(_loggerRepository.Name, name), this);
        }

        public override ILogger Create(string name, LoggerLevel level)
        {
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }
    }
}
