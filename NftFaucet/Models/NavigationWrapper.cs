using Microsoft.AspNetCore.Components;

namespace NftFaucet.Models;

public class NavigationWrapper
{
    private readonly NavigationManager _uriHelper;
    private string _currentUri;
    private int _currentStep;
    private Func<Task<bool>> _beforeGoBack;
    private Func<Task<bool>> _beforeGoForward;

    public NavigationWrapper(NavigationManager uriHelper)
    {
        _uriHelper = uriHelper;
    }

    public int CurrentStep
    {
        get
        {
            var uri = _uriHelper.ToBaseRelativePath(_uriHelper.Uri);
            if (uri == _currentUri)
                return _currentStep;

            _currentUri = uri;
            if (!_currentUri.StartsWith("step") || uri.Length < 5)
            {
                _currentStep = 1;
            }
            else
            {
                _currentStep = int.Parse(uri.Substring(4).First().ToString());
            }

            return _currentStep;
        }
    }

    public void SetBackHandler(Func<Task<bool>> handler)
    {
        _beforeGoBack = handler;
    }

    public void SetForwardHandler(Func<Task<bool>> handler)
    {
        _beforeGoForward = handler;
    }

    public async Task GoBack()
    {
        if (_beforeGoBack != null)
        {
            var shouldGoBack = await _beforeGoBack();
            if (!shouldGoBack)
                return;
        }

        var previousStep = CurrentStep - 1;
        _beforeGoBack = null;
        _beforeGoForward = null;
        _uriHelper.NavigateTo("/step" + previousStep);
    }

    public async Task GoForward()
    {
        if (_beforeGoForward != null)
        {
            var shouldGoForward = await _beforeGoForward();
            if (!shouldGoForward)
                return;
        }

        var nextStep = CurrentStep + 1;
        _beforeGoBack = null;
        _beforeGoForward = null;
        _uriHelper.NavigateTo("/step" + nextStep);
    }
}
