using System;
using System.Text;
using System.Linq;
using Lime.Protocol;
using Take.Blip.Client;
using System.Threading;
using System.Globalization;
using System.Threading.Tasks;
using Lime.Messaging.Contents;
using Take.Blip.Client.Session;
using System.Collections.Generic;
using Take.Blip.Client.Extensions.Bucket;

namespace desafio
{
    public class StateMachine
    {

        private readonly IStateManager _stateManager;

        private readonly ISender _sender; 
        
        private readonly IDictionary<string, CarrasoulLoading> _carrasoulLoading;


        public StateMachine(ISender sender, IStateManager stateManager)
        {
            _sender = sender;
            _stateManager = stateManager;
            _carrasoulLoading = new Dictionary<string, CarrasoulLoading>();
        }

        public async Task<string> VerifyStateAsync(Message message, CancellationToken cancellationToken)
        {
            var currentState = await _stateManager.GetStateAsync(message.From.ToIdentity(), cancellationToken);
            return currentState == "default" ? "Inicio" : currentState;
        }

        public async Task RunAsync(Message message, CancellationToken cancellationToken, ChatState chatState)
        {
            CarrasoulLoading carrasoulLoading = _carrasoulLoading.ContainsKey(message.From.ToIdentity().ToString()) ? carrasoulLoading = _carrasoulLoading[message.From.ToIdentity().ToString()] : new CarrasoulLoading();

            switch (await VerifyStateAsync(message, cancellationToken))
            {
                case "Inicio":

                    await _sender.SendMessageAsync(chatState, message.From, cancellationToken);
                    Thread.Sleep(500);
                    await _sender.SendMessageAsync("Olá, espero que esteja tudo. Sou a Lora responsável por passar os valores da Take.", message.From, cancellationToken);
                    Thread.Sleep(500);
                    await _sender.SendMessageAsync(chatState, message.From, cancellationToken);
                    Thread.Sleep(500);
                    await _sender.SendMessageAsync("Mesmo eu sendo um robô e não tendo consciência sobre o que é valores, posso te ajudar de alguma forma.", message.From, cancellationToken);
                    Thread.Sleep(500);
                    await _sender.SendMessageAsync(chatState, message.From, cancellationToken);
                    Thread.Sleep(500);
                    await _sender.SendMessageAsync("Vamos lá...", message.From, cancellationToken);
                    Thread.Sleep(500);
                    await _sender.SendMessageAsync(chatState, message.From, cancellationToken);
                    Thread.Sleep(500);
                    await _sender.SendMessageAsync("A cultura Take tem como base 6 valores que são:", message.From, cancellationToken);
                    Thread.Sleep(500);
                    await _sender.SendMessageAsync(chatState, message.From, cancellationToken);
                    Thread.Sleep(500);
                    await _sender.SendMessageAsync(carrasoulLoading.Culturas(), message.From, cancellationToken);

                    await _stateManager.SetStateAsync(message.From, "Inicio", cancellationToken);

                    break;

                default:
                    
                    await _sender.SendMessageAsync(chatState, message.From, cancellationToken);
                    Thread.Sleep(1000);
                    await _sender.SendMessageAsync("Desculpe, eu não entendo.", message.From, cancellationToken);

                    await _stateManager.SetStateAsync(message.From, await VerifyStateAsync(message, cancellationToken), cancellationToken);
                    
                    break;
            }

        }

    }
}
