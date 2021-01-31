using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    [Validator(typeof(PlayerValidator))]
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }

    }

    public class PlayerValidator : AbstractValidator<Player>
    {
        public PlayerValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(0, 10);
            RuleFor(x => x.Age).InclusiveBetween(18, 60);
        }
    }
}