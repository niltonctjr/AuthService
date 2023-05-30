using CommandLine;
using FluentMigrator.Runner;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Migration
{
    public class ConsoleApp
    {
        private readonly ILogger<ConsoleApp> _logger;
        private readonly IMigrationRunner _migrationRunner;

        public ConsoleApp(ILogger<ConsoleApp> logger,
            IMigrationRunner migrationRunner)
        {
            _logger = logger;
            _migrationRunner = migrationRunner;
        }

        public void Run(string[] args)
        {
            _logger.LogInformation("Iniciando a execucao das Migrations...");
            var command = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;

            try
            {
                if (command.IsUp())
                    _migrationRunner.MigrateUp();
                else
                    _migrationRunner.MigrateDown(command.Version);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Erro durante a execucao das Migrations: {ex.Message} | {ex.GetType().Name}");
            }

            _logger.LogInformation("Verificacao das Migrations concluida.");
        }
    }

    public class CommandLineOptions
    {
        [Option('c', "commad", Required = true, HelpText = "Informe Up para subir as migrações, Down para descer a migração")]
        public string? Command { get; set; }

        [Option('v', "version", Required = false, HelpText = "Informe a versão para qual o deseja realzar o downgrade")]
        public long Version { get; set; }

        public bool IsUp()
        {
            return Command == null || (Command != null && Command.ToUpper() != "DOWN");
        }
    }
}
