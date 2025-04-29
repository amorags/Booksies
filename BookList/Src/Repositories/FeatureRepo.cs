using Microsoft.EntityFrameworkCore;
using BookList.Domain;
using BookList.Data;
using BookList.Repository.Interfaces;

public class FeatureToggleRepository
{
    private readonly BookContext _context;

    public FeatureToggleRepository(BookContext context)
    {
        _context = context;
    }

    public async Task<bool> IsEnabledAsync(string featureName)
    {
        var toggle = await _context.FeatureToggle
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Name == featureName);

        return toggle?.IsEnabled ?? false;
    }

    public async Task SetFeatureAsync(string featureName, bool enabled)
    {
        var toggle = await _context.FeatureToggle.FirstOrDefaultAsync(f => f.Name == featureName);
        if (toggle == null)
        {
            _context.FeatureToggle.Add(new FeatureToggle { Name = featureName, IsEnabled = enabled });
        }
        else
        {
            toggle.IsEnabled = enabled;
        }
        await _context.SaveChangesAsync();
    }
}