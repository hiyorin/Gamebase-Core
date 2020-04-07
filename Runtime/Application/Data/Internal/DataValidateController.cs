using UnityEngine;
using Zenject;

namespace Gamebase.Application.Data.Internal
{
    internal sealed class DataValidateController : IDataValidateController
    {
        private readonly IValidateRepository[] repositories;
        
        public DataValidateController([InjectOptional] IValidateRepository[] repositories)
        {
            this.repositories = repositories;
        }
        
        public void Validate()
        {
            if (repositories == null)
            {
                Debug.unityLogger.LogWarning(GetType().Name, "Not found validate repositories");
                return;
            }

            Debug.unityLogger.Log(GetType().Name, "Start Validate");
            foreach (var repository in repositories)
            {
                Debug.unityLogger.Log(GetType().Name, $"Validating {repository.GetType()}");
                repository.Validate();
            }
            Debug.unityLogger.Log(GetType().Name, "End Validate");
        }
    }
}