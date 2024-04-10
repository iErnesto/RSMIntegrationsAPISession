using RSMEnterpriseIntegrationsAPI.Application.DTOs;
using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
using RSMEnterpriseIntegrationsAPI.Domain.Models;

namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    public class SalesOrderHeaderService(ISalesOrderHeaderRepository repository) : ISalesOrderHeaderService
    {
        private readonly ISalesOrderHeaderRepository _salesOrderHeaderRepository = repository;

        public async Task<IEnumerable<GetSalesOrderHeaderDto>> GetAll()
        {
            var salesOrderHeaders = await _salesOrderHeaderRepository.GetSalesOrderHeaders();
            if (salesOrderHeaders == null)
            {
                return [];
            }

            List<GetSalesOrderHeaderDto> salesOrderHeaderDtos = [];
            foreach (var salesOrderHeader in salesOrderHeaders)
            {
                GetSalesOrderHeaderDto dto = new()
                {
                    SalesOrderId = salesOrderHeader.SalesOrderId,
                    RevisionNumber = salesOrderHeader.RevisionNumber,
                    OrderDate = salesOrderHeader.OrderDate,
                    DueDate = salesOrderHeader.DueDate,
                    ShipDate = salesOrderHeader.ShipDate,
                    Status = salesOrderHeader.Status,
                    SubTotal = salesOrderHeader.SubTotal,

                };
                salesOrderHeaderDtos.Add(dto);
            }
            return salesOrderHeaderDtos;
        }


        public async Task<GetSalesOrderHeaderDto?> GetSalesOrderHeaderById(int id)
        {
            if (id == 0)
            {
                throw new BadRequestException("SalesOrderHeade Id is not valid");
            }

            var salesOrderHeader = await ValidateSalesOrderHeaderExistence(id);

            GetSalesOrderHeaderDto dto = new()
            {
                SalesOrderId = salesOrderHeader.SalesOrderId,
                RevisionNumber = salesOrderHeader.RevisionNumber,
                OrderDate = salesOrderHeader.OrderDate,
                DueDate = salesOrderHeader.DueDate,
                ShipDate = salesOrderHeader.ShipDate,
                Status = salesOrderHeader.Status,
                SubTotal = salesOrderHeader.SubTotal,
            };
            return dto;

        }

        public async Task<int> CreateSalesOrderHeader(CreateSalesOrderHeaderDto createSalesOrderHeaderDto)
        {
            if (createSalesOrderHeaderDto is null)
            {
                throw new BadRequestException("SalesOrderHeader is not valid");
            }

            SalesOrderHeader salesOrderHeader = new()
            {
                RevisionNumber = createSalesOrderHeaderDto.RevisionNumber,
                OrderDate = createSalesOrderHeaderDto.OrderDate,
                DueDate = createSalesOrderHeaderDto.DueDate,
                Status = createSalesOrderHeaderDto.Status,
                OnlineOrderFlag = createSalesOrderHeaderDto.OnlineOrderFlag,
                SalesOrderNumber = createSalesOrderHeaderDto.SalesOrderNumber,
                PurchaseOrderNumber = createSalesOrderHeaderDto.PurchaseOrderNumber,
                AccountNumber = createSalesOrderHeaderDto.AccountNumber,
                SubTotal = createSalesOrderHeaderDto.SubTotal,
                TaxAmt = createSalesOrderHeaderDto.TaxAmt,
                Freight = createSalesOrderHeaderDto.Freight,
                TotalDue = createSalesOrderHeaderDto.TotalDue,
                Rowguid = createSalesOrderHeaderDto.Rowguid

            };
            return await _salesOrderHeaderRepository.CreateSalesOrderHeader(salesOrderHeader);
        }

        public async Task<int> UpdateSalesOrderHeader(UpdateSalesOrderHeaderDto updateSalesOrderHeaderDto)
        {
            if (updateSalesOrderHeaderDto == null)
            {
                throw new BadRequestException("Sales order header information is not valid.");
            }

            var salesOrderHeader = await ValidateSalesOrderHeaderExistence(updateSalesOrderHeaderDto.SalesOrderId);

            salesOrderHeader.RevisionNumber = updateSalesOrderHeaderDto.RevisionNumber;
            salesOrderHeader.OrderDate = updateSalesOrderHeaderDto.OrderDate;
            salesOrderHeader.DueDate = updateSalesOrderHeaderDto.DueDate;
            salesOrderHeader.ShipDate = updateSalesOrderHeaderDto.ShipDate;
            salesOrderHeader.Status = updateSalesOrderHeaderDto.Status;
            salesOrderHeader.OnlineOrderFlag = updateSalesOrderHeaderDto.OnlineOrderFlag;
            salesOrderHeader.SalesOrderNumber = updateSalesOrderHeaderDto.SalesOrderNumber;
            salesOrderHeader.PurchaseOrderNumber = updateSalesOrderHeaderDto.PurchaseOrderNumber;
            salesOrderHeader.AccountNumber = updateSalesOrderHeaderDto.AccountNumber;
            salesOrderHeader.CreditCardApprovalCode = updateSalesOrderHeaderDto.CreditCardApprovalCode;
            salesOrderHeader.SubTotal = updateSalesOrderHeaderDto.SubTotal;
            salesOrderHeader.TaxAmt = updateSalesOrderHeaderDto.TaxAmt;
            salesOrderHeader.Freight = updateSalesOrderHeaderDto.Freight;
            salesOrderHeader.TotalDue = updateSalesOrderHeaderDto.TotalDue;
            salesOrderHeader.Comment = updateSalesOrderHeaderDto.Comment;
            salesOrderHeader.Rowguid = updateSalesOrderHeaderDto.Rowguid;

            return await _salesOrderHeaderRepository.UpdateSalesOrderHeader(salesOrderHeader);
        }

        public async Task<int> DeleteSalesOrderHeader(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException($"Unable to delete salesOrderHeader with id {id}");
            }
            var salesOrderHeader = await ValidateSalesOrderHeaderExistence(id);
            return await _salesOrderHeaderRepository.DeleteSalesOrderHeader(salesOrderHeader);

        }
        private async Task<SalesOrderHeader> ValidateSalesOrderHeaderExistence(int id)
        {
            var existingSalesOrderHeader = await _salesOrderHeaderRepository.GetSalesOrderHeader(id)
                ?? throw new NotFoundException($"Sales Order Header with Id: {id} was not found.");

            return existingSalesOrderHeader;
        }
    }
}
