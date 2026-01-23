namespace Erp_Blazor.Service.States
{
    public class EntiteState
    {
        private int? _selectedEntiteId;

        public int? SelectedEntiteId
        {
            get => _selectedEntiteId;
            set
            {
                if (_selectedEntiteId != value)
                {
                    _selectedEntiteId = value;
                    NotifyStateChanged();
                }
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
