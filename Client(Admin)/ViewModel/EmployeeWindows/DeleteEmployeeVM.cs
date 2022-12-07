using Client_Admin_.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel.EmployeeWindows
{
    public class DeleteEmployeeVM : BaseVM
    {
		private ObservableCollection<Employee> _employees;

		public ObservableCollection<Employee> Employees
		{
			get { return _employees; }
			set 
			{ 
				_employees = value; 
				OnPropertyChanged("Employees");
			}
		}

		private Employee _selectedEmployee;

		public Employee SelectedEmployee
		{
			get { return _selectedEmployee; }
			set 
			{ 
				_selectedEmployee = value;
				OnPropertyChanged("SelectedEmployee");
			}
		}

		private RelayCommand _deleteCommand;

		public RelayCommand DeleteCommand
		{
			get { return _deleteCommand ?? new RelayCommand(DeleteUser); }

		}


		public DeleteEmployeeVM()
		{
			Employees = new ObservableCollection<Employee>();
			MainVM.GetInstance().ServerClient.GetEmployees += GetEmployees;
			MainVM.GetInstance().ServerClient.SendQuerryForEmployees();
		}

		private void DeleteUser()
		{
			if (SelectedEmployee == null)
				return;
			MainVM.GetInstance().ServerClient.SendMessageToServer($"--removeEmployee\nemployeeId={SelectedEmployee.Id}\n");
			Employees.Remove(SelectedEmployee);
			SelectedEmployee = null;
		}

		private void GetEmployees(IEnumerable<Employee> obj)
		{
			Employees.Clear();
			foreach (var item in obj)
			{
				Employees.Add(item);
			}
		}
	}
}
