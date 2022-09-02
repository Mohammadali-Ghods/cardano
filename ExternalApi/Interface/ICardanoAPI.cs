using ExternalApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApi.Interface
{
    public interface ICardanoAPI
    {
        Task<CardanoAPIModel> GetRecord(string lei);
    }
}
