using System;
using Android.Views;
using Android.Widget;

namespace XamarinNativeTemplate.Droid
{
    public class MyTableCellAdapter : BaseAdapter<Employee>
    {
        readonly MyTableViewActivity _context;

        public MyTableCellAdapter(MyTableViewActivity pContext)
        {
            _context = pContext;
        }

        public override Employee this[int position] => _context.MainVm.AllEmployees[position];

        public override int Count => _context.MainVm.AllEmployees.Count;

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var employeeItem = _context.MainVm.AllEmployees[position];

            var view = convertView;

            MyTableCellViewHolder myTableCellViewHolder = null;

            if (view != null)
                myTableCellViewHolder = view.Tag as MyTableCellViewHolder;

            if (myTableCellViewHolder == null)
            {
                myTableCellViewHolder = new MyTableCellViewHolder();

                var inflater = LayoutInflater.From(_context);

                view = inflater.Inflate(Resource.Layout.MyTableViewCellLayout, parent, false);

                myTableCellViewHolder.EmployeeName = view.FindViewById<TextView>(Resource.Id.empNameTextView);

                myTableCellViewHolder.EmployeePhone = view.FindViewById<TextView>(Resource.Id.empPhoneTextView);
                myTableCellViewHolder.EmployeeDOB = view.FindViewById<TextView>(Resource.Id.empDOBTextView);
                myTableCellViewHolder.EmailId = view.FindViewById<TextView>(Resource.Id.empEmailTextView);

                view.Tag = myTableCellViewHolder;
            }

            myTableCellViewHolder.EmployeeName.Text = employeeItem.Name;
            myTableCellViewHolder.EmployeePhone.Text = employeeItem.Phone;
            myTableCellViewHolder.EmployeeDOB.Text = employeeItem.EmployeeDOB;
            myTableCellViewHolder.EmailId.Text = employeeItem.EmailId;

            return view;
        }
    }

    public class MyTableCellViewHolder : Java.Lang.Object
    {
        public TextView EmployeeName { get; set; }

        public TextView EmailId { get; set; }
        public TextView EmployeePhone { get; set; }
        public TextView EmployeeDOB { get; set; }
    }
}