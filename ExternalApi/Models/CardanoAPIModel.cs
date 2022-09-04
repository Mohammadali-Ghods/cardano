using ExternalApi.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApi.Models
{
    public class CardanoAPIModel
    {
        public List<Data> data { get; set; }
        public string Error { get; set; }

        public ValidationResult IsValid()
        {
            var validationResult = new CardanoAPIModelValidation().Validate(this);
            if (!validationResult.IsValid)
                Error = string.Join
                               ("\n", validationResult.Errors.Select(x => x.ErrorMessage).ToArray());
            return new CardanoAPIModelValidation().Validate(this);
        }
    }
    public class Data 
    {
        public Attributes attributes { get; set; }
    }
    public class Attributes 
    {
        public string lei { get; set; }
        public Entity entity { get; set; }
        public List<string> bic { get; set; }
    }
    
    public class Entity 
    {
        public LegalName legalName { get; set; }
        public LegalAddress legalAddress { get; set; }
    }
    public class LegalAddress 
    {
        public string country { get; set; }
    }
    public class LegalName 
    {
        public string name { get; set; }
        public string language { get; set; }
    }
}
