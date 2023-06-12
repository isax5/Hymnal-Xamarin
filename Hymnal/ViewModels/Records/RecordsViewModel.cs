namespace Hymnal.ViewModels
{
    public sealed class RecordsViewModel : BaseViewModel
    {
        private readonly HymnsService hymnsService;
        private readonly DatabaseService databaseService;


        public RecordsViewModel(HymnsService hymnsService, DatabaseService databaseService)
        {
            this.hymnsService = hymnsService;
            this.databaseService = databaseService;
        }

        public override void Initialize()
        {
            base.Initialize();

            LoadData();
        }


        private void LoadData()
        {

        }
    }
}
