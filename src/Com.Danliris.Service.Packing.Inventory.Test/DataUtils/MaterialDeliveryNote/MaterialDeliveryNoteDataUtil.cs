﻿
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.MaterialDeliveryNote
{
   public class MaterialDeliveryNoteDataUtil : BaseDataUtil<MaterialDeliveryNoteRepository, MaterialDeliveryNoteModel>
    {
        public MaterialDeliveryNoteDataUtil(MaterialDeliveryNoteRepository repository) : base(repository)
        {

        }

        public override MaterialDeliveryNoteModel GetModel()
        {
            return new MaterialDeliveryNoteModel(
                "code",
                DateTimeOffset.Now,
                "boncode",
                 DateTimeOffset.Now.AddDays(-2),
                 DateTimeOffset.Now.AddDays(2),
                 "donumber",
                 "fonumber",
                 "receiver",
                 "remark",
                 "scnumber",
                 "sender",
                 "storageNumber",
                 new List<ItemsModel>()
                 {
                     new ItemsModel(
                         "noSPP",
                         "materialName",
                         "inputLot",
                         1,
                         1,
                         1,
                         1,
                         1
                         )
                 }

                );
        }

        public override MaterialDeliveryNoteModel GetEmptyModel()
        {
            return new MaterialDeliveryNoteModel(
                "code",
                DateTimeOffset.Now,
                "boncode",
                 DateTimeOffset.Now.AddDays(-2),
                 DateTimeOffset.Now.AddDays(2),
                 "donumber",
                 "fonumber",
                 "receiver",
                 "remark",
                 "scnumber",
                 "sender",
                 "storageNumber",
                 new List<ItemsModel>()
                 {
                     new ItemsModel(
                         "noSPP",
                         "materialName",
                         "inputLot",
                         1,
                         1,
                         1,
                         1,
                         1
                         )
                 }

                );
        }
    }
}
