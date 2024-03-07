public class FocusManager
{
    private InteractableItem _currentObject;
    private InteractableItem _previousObject;

    public void UpdateFocus(InteractableItem newFocus)
    {
        if (newFocus != _currentObject && _currentObject != null)
        {
            RemovePreviousFocus();
        }
        SetCurrentFocus(newFocus);
    }

    private void RemovePreviousFocus()
    {
        _previousObject = _currentObject;
        _previousObject.RemoveFocus();
    }

    private void SetCurrentFocus(InteractableItem newFocus)
    {
        _currentObject = newFocus;
        _currentObject?.SetFocus();
    }
}