using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using WebApplicationTest.Context;
using System.Web.Mvc;
using WebApplicationTest.Models;

namespace WebApplicationTest.Attribute
{
    public class PlayersValidationProvider: ModelValidatorProvider
    {

        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            if (metadata.ContainerType == typeof(Player))
            {
                return new ModelValidator[]
                {
                    new PlayerProperyValidator(metadata,context)
                };
                
            }
            return Enumerable.Empty<ModelValidator>();
            
        }
    }

    
    public class PlayerProperyValidator : ModelValidator
    {
        public PlayerProperyValidator(ModelMetadata metadata, ControllerContext context):base(metadata,context)
        { }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            Player p = container as Player;
            if(p != null)
            {
                switch (Metadata.PropertyName)
                {
                    case nameof(Player.Name):
                        if (string.IsNullOrEmpty(p.Name))
                        {
                            return new ModelValidationResult[]
                            {
                                new ModelValidationResult {  MemberName="Name", Message="Введите имя игрока"}
                            };
                        }


                        break;
                    case nameof(Player.Age):
                        if (p.Age == 0)
                        {
                            return new ModelValidationResult[]
                            {
                                new ModelValidationResult {  MemberName="Age", Message="Введите возраст игрока"}
                            };
                        }
                        
                        break;

                }
            }

            return Enumerable.Empty<ModelValidationResult>();
        }
    }

}