using Discount.Grpc;
using Discount.GrpcService.Data;
using Discount.GrpcService.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.GrpcService.Services
{
    public class DiscountService(DiscountDbContext dbcontext, ILogger<DiscountService> logger): DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest requenst, ServerCallContext context)
        {
            var discount = await dbcontext.Coupon.FirstOrDefaultAsync(x=> x.ProductName == requenst.ProductName);
            if (discount == null)
                discount =  new Coupon() { ProductName = "No Coupon", Description = "No Coupon Description", Amount = 0, Id = 0 };

            logger.LogInformation($"Coupon with ProductName = {discount.ProductName}, Description = {discount.Description}, Amount = {discount.Amount}, Id= {discount.Id} is retrived.");

            var couponModel =  discount.Adapt<CouponModel>();
            return couponModel;
            
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if(coupon == null)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Data"));
            dbcontext.Add(coupon);
            await dbcontext.SaveChangesAsync();

            logger.LogInformation($"New coupon created with product Name = {coupon.ProductName}");

            var newCoupon = coupon.Adapt<CouponModel>();
            return newCoupon;

        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Data"));
            dbcontext.Update<Coupon>(coupon);
            await dbcontext.SaveChangesAsync();

            logger.LogInformation($"Coupon for Product Name = {coupon.ProductName} updated successfully.");

            var updatedCoupon = coupon.Adapt<CouponModel>();
            return updatedCoupon;

        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            if(string.IsNullOrEmpty(request.ProductName))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Data"));
            
            var discount = await dbcontext.Coupon.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if(discount == null)
                throw new RpcException(new Status(StatusCode.NotFound, "No Coupon Found"));

            dbcontext.Coupon.Remove(discount);
            await dbcontext.SaveChangesAsync();

            logger.LogInformation($"Coupon with Name = {request.ProductName} is  deleted.");

            return new DeleteDiscountResponse() { IsSuccess = true };

        }
    }
}
