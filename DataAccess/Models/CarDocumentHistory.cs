﻿using System.Collections.Generic;
using DataAccess.Core.Abstractions;

namespace DataAccess.Models
{
  public abstract class CarDocumentHistoryBase : DataModelBase, ICarDocumentHistoryBase
  {
    public int CarId { get; set; }

    public List<ICarDocumentBase> Docs { get; set; }
  }
}