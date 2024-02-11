using CustomerService.Application.Exceptions;
using CustomerService.Application.Logging;
using CustomerService.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.UseCaseHandiling
{
    public class CommandHandler : ICommandHandler
    {
        private IApplicationActor _actor;
        private IUseCaseLogger _logger;

        public CommandHandler(IApplicationActor actor, IUseCaseLogger logger)
        {
            _actor = actor;
            _logger = logger;
        }

        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest data)
        {
            if (!_actor.AllowedUseCases.Contains(command.Id))
            {
                throw new UnauthorizedUseCaseExecutionException(_actor.Username, command.Name);

            }

            _logger.Add(new UseCaseLogEntry
            {

                Actor = _actor.Username,
                ActorId = _actor.Id,
                Data = data,
                UseCaseName = command.Name

            });

            command.Execute(data);

            Console.WriteLine("UseCase: " + command.Name + " User: " + _actor.Username);
        }

       
    }
}
