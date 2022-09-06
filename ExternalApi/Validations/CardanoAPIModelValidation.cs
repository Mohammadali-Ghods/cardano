using ExternalApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApi.Validations
{
    public class CardanoAPIModelValidation : AbstractValidator<CardanoAPIModel>
    {
        public CardanoAPIModelValidation()
        {
            RuleFor(x => x).NotNull()
                .WithMessage("The Cardano API has not any specific response for this records");

            RuleFor(x => x.data).NotNull().NotEmpty()
                .WithMessage("The Cardano API has not any specific response for this records / data");
        }
    }
}
