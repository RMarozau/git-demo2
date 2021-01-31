using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationTest.Models;

namespace WebApplicationTest.Attribute
{
    public class TeamValidationProvider: ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            if (metadata.ContainerType == typeof(Team))
            {
                return new ModelValidator[]
                {
                    new TeamProperyValidator(metadata,context)
                };

            }
            return Enumerable.Empty<ModelValidator>();

        }
    }

    public class TeamProperyValidator : ModelValidator
    {
        public TeamProperyValidator(ModelMetadata metadata, ControllerContext context) : base(metadata, context)
        { }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            Team t = container as Team;
            if (t != null)
            {
                switch (Metadata.PropertyName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(t.Name))
                        {
                            return new ModelValidationResult[]
                            {
                                new ModelValidationResult {  MemberName="Name", Message="Введите имя команды"}
                            };
                        }


                        break;
                    case "Coach":
                        if (string.IsNullOrEmpty(t.Coach))
                        {
                            return new ModelValidationResult[]
                            {
                                new ModelValidationResult {  MemberName="Coach", Message="Введите треннера команды"}
                            };
                        }

                        break;

                }
            }

            return Enumerable.Empty<ModelValidationResult>();
        }
    }
}