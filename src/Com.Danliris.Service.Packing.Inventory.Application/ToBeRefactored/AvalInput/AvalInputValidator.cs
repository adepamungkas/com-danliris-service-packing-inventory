﻿using FluentValidation;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalInput
{
    public class AvalInputValidator : AbstractValidator<AvalInputViewModel>
    {
        public AvalInputValidator()
        {
            RuleFor(inventoryDocument => inventoryDocument.Id).NotEmpty().WithMessage("Id Tidak Valid");
            //RuleFor(inventoryDocument => inventoryDocument.BonNo).NotNull().WithMessage("No. Bon Harus Diisi");
            RuleFor(inventoryDocument => inventoryDocument.Shift).NotEmpty().WithMessage("Shift Harus Diisi");
            RuleFor(inventoryDocument => inventoryDocument.UOMUnit).NotEmpty().WithMessage("Satuan Harus Diisi");
            RuleFor(inventoryDocument => inventoryDocument.ProductionOrderQuantity).NotNull().WithMessage("Qty Satuan Harus Diisi");
            RuleFor(inventoryDocument => inventoryDocument.QtyKg).NotNull().WithMessage("Qty KG Harus Diisi");
        }
    }
}
