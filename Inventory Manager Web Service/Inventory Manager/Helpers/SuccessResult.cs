namespace Inventory_Manager.Helpers
{
    class SuccessResult<T> : ResultBase
    {
        public bool success { get; set; }
        public T result { get; set; }
    }
}
