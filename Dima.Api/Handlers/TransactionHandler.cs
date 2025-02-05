using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Dima.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{
    public async Task<Response<Transaction>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction()
            {
                Amount = request.Amount,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.UtcNow,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                Title = request.Title,
                Type = request.Type
            };
             await context.Transactions.AddAsync(transaction);
             await context.SaveChangesAsync();
             return new Response<Transaction>(transaction,201, "Transação criada com sucesso.");
        }
        catch (Exception ex) 
        {
            return new Response<Transaction>(null, 500, $"Erro ao criar Transação.{ex.Message}");
        }
    }

    public async Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await context.
                                    Transactions.
                                    FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction is null)
                return new Response<Transaction>(null, 404, "Transação não localizada");

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();
            return new Response<Transaction>(null, 204, "Transação excluida com sucesso.");

        }
        catch (Exception ex) 
        {
            return new Response<Transaction>(null, 500, $"Erro ao excluir Transação.{ex.Message}");
        }
    }

    public async Task<Response<Transaction>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await context.
                                    Transactions.
                                    AsNoTracking().
                                    FirstOrDefaultAsync(x => x.Id==request.Id && x.UserId == request.UserId);

            return transaction is null ? 
                new Response<Transaction>(null,404,"Transação não localizada"): 
                new Response<Transaction>(transaction, 200);
        }
        catch (Exception ex) 
        {
            return new Response<Transaction>(null, 505, $"Erro ao procurar Transação.{ex.Message}");
        }
    }

    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        try
        {
            var dateFirst = DateTime.Now.GetFirstDay();
            var dateLast =  DateTime.Now.GetLastDay();

            var query =  context.
                         Transactions.
                         AsNoTracking().
                         Where(x => x.UserId == request.UserId && 
                                    x.PaidOrReceivedAt >= dateFirst &&
                                    x.PaidOrReceivedAt <= dateLast).
                         OrderBy(x => x.CreatedAt);

            var count = await query.CountAsync();

            var transactions = await query
                                     .Skip((request.PagedNumber - 1) * request.PageSize)
                                     .Take(request.PageSize)
                                     .ToListAsync();

           return new PagedResponse<List<Transaction>?>(transactions, 200,count);

        }
        catch (Exception ex)
        {
            return new PagedResponse<List<Transaction>?>(null, 505, $"Erro ao procurar Transação.{ex.Message}");
        }
    }

    public async Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await context.
                                    Transactions.
                                    FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (transaction is null)
                return new Response<Transaction>(null, 404, "Transação não localizada");

            transaction.Title = request.Title;
            transaction.Amount = request.Amount;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
            transaction.CategoryId = request.CategoryId;
            transaction.Type = request.Type;
            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();
            return new Response<Transaction>(transaction, 200, "Transação atualizada com sucesso.");
        }
        catch (Exception ex) 
        {
            return new Response<Transaction>(null, 500, $"Erro ao atualizar Transação.{ex.Message}");
        }
    }
}
