using Coravel.Invocable;

namespace CoingeckoLogic.Jobs;

public class ProcessCoinsData : IInvocable
{
    private readonly CoingeckoService coingeckoService;

    public ProcessCoinsData(CoingeckoService coingeckoService) 
    {
        this.coingeckoService = coingeckoService;   
    }

    public async Task Invoke()
    {
        await coingeckoService.ProcessCoinsData();
    }
}
