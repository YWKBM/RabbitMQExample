using Coravel.Invocable;

namespace CoingeckoLogic.Jobs;

public class GetCoinsDataJob : IInvocable
{
    private readonly CoingeckoService coingeckoService;

    public GetCoinsDataJob(CoingeckoService coingeckoService) 
    {
        this.coingeckoService = coingeckoService;
    }

    public async Task Invoke()
    {
        Console.WriteLine("Staring GetCoinsDataJob");

        try
        {
            await coingeckoService.GetCoinsData();

            Console.WriteLine("GetCoinsDataJob done");
        }
        catch (Exception e) 
        {
            Console.WriteLine(e.Message);
        }
    }
}
