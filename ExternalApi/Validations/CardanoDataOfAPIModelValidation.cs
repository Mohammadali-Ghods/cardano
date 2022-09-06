using ExternalApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApi.Validations
{
    public class CardanoDataOfAPIModelValidation : AbstractValidator<Data>
    {
        public CardanoDataOfAPIModelValidation()
        {
            RuleFor(x => x).NotNull()
                          .WithMessage("The Cardano API has not any specific response for this record");

            RuleFor(x => x.attributes).NotNull()
        .WithMessage("The Cardano API has not any specific response for this record / attribute");

            RuleFor(x => x.attributes.bic).NotNull()
               .WithMessage("The Cardano API has not bic value for this record");

            RuleFor(x => x.attributes.entity).NotNull()
             .WithMessage("The Cardano API send null or empty value for entity object that has country" +
             " and as you know the country is very importtant for calculate transaction cost");

            RuleFor(x => x.attributes.entity.legalName).NotNull()
            .WithMessage("The Cardano API send null or empty value for legal name object that has" +
            " legalname and language of this record");

            List<string> countrylistthathasformula = new List<string>() { "GB", "NL" };

            RuleFor(x => x.attributes.entity.legalAddress).NotNull()
          .WithMessage("The Cardano API send null or empty value for legalAddress object that has country" +
             " and as you know the country is very importtant for calculate transaction cost");

            RuleFor(x => x.attributes.entity.legalAddress.country).NotNull().NotEmpty()
         .WithMessage("The Cardano API send null or empty value for country value " +
            " and as you know the country is very importtant for calculate transaction cost")
         .Must(x => countrylistthathasformula.Contains(x)).WithMessage("Our system doesn't " +
         "support this record country and we don't have any specific formula for this record");
        }


    }
}
