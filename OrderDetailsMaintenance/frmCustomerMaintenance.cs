using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrderDetailsMaintenance.Models.DataLayer;

namespace OrderDetailsMaintenance;

public partial class frmCustomerMaintenance : Form
{
    private readonly NorthwindContext _context = new();
    private Customer? _customer;

    // Opeyemi Obute
    public frmCustomerMaintenance()
    {
        InitializeComponent();
        txtCustomerId.MaxLength = 5;
        txtContact.MaxLength = 30;
        txtAddress.MaxLength = 60;
        txtCity.MaxLength = 15;
        txtCountry.MaxLength = 15;
        txtCustomerId.TabIndex = 0;
        btnFind.TabIndex = 1;
        txtContact.TabIndex = 2;
        txtAddress.TabIndex = 3;
        txtCity.TabIndex = 4;
        txtCountry.TabIndex = 5;
        btnSave.TabIndex = 6;
        btnExit.TabIndex = 7;
        AcceptButton = btnFind;
        CancelButton = btnExit;
        btnFind.Click += btnFind_Click;
        btnSave.Click += btnSave_Click;
        btnExit.Click += btnExit_Click;
        txtCustomerId.TextChanged += txtCustomerId_TextChanged;
        ClearCustomer();
    }

    // Opeyemi Obute
    private void btnFind_Click(object? sender, EventArgs e)
    {
        string id = txtCustomerId.Text.Trim().ToUpperInvariant();
        txtCustomerId.Text = id;
        ClearCustomer();
        if (id.Length != 5)
        {
            MessageBox.Show("Enter a five-character customer ID.", "Customer ID");
            txtCustomerId.Focus();
            return;
        }
        try
        {
            _context.ChangeTracker.Clear();
            _customer = _context.Customers.Find(id);
            if (_customer == null)
            {
                MessageBox.Show($"No customer found for {id}.", "Customer not found");
                return;
            }
            txtContact.Text = _customer.ContactName;
            txtAddress.Text = _customer.Address;
            txtCity.Text = _customer.City;
            txtCountry.Text = _customer.Country;
            SetEditingEnabled(true);
        }
        catch (SqlException ex)
        {
            MessageBox.Show($"Unable to find the customer.\n{ex.Message}", "Database error");
        }
    }

    // Opeyemi Obute
    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (_customer == null) return;
        try
        {
            _customer.ContactName = OptionalText(txtContact);
            _customer.Address = OptionalText(txtAddress);
            _customer.City = OptionalText(txtCity);
            _customer.Country = OptionalText(txtCountry);
            _context.Customers.Update(_customer);
            _context.SaveChanges();
            MessageBox.Show("Customer saved successfully.", "Customer saved");
        }
        catch (DbUpdateException ex)
        {
            MessageBox.Show($"Unable to save the customer.\n{ex.GetBaseException().Message}",
                "Database error");
        }
    }

    // Opeyemi Obute
    private void btnExit_Click(object? sender, EventArgs e)
    {
        Close();
    }

    // Opeyemi Obute
    private void txtCustomerId_TextChanged(object? sender, EventArgs e)
    {
        ClearCustomer();
    }

    // Opeyemi Obute
    private void ClearCustomer()
    {
        _customer = null;
        txtContact.Clear();
        txtAddress.Clear();
        txtCity.Clear();
        txtCountry.Clear();
        SetEditingEnabled(false);
    }

    // Opeyemi Obute
    private void SetEditingEnabled(bool enabled)
    {
        txtContact.Enabled = txtAddress.Enabled = enabled;
        txtCity.Enabled = txtCountry.Enabled = enabled;
        btnSave.Enabled = enabled;
    }

    // Opeyemi Obute
    private static string? OptionalText(TextBox textBox)
    {
        return string.IsNullOrWhiteSpace(textBox.Text) ? null : textBox.Text.Trim();
    }
}
