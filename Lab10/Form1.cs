using Lab10;
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;

namespace Lab10
{
    public partial class Form1 : Form
    {
        private BookDAL bookDAL = new BookDAL();
        private List<Book> bookList;

        public Form1()
        {
            InitializeComponent();
        }

        // FORM LOAD
        private void Form1_Load(object sender, EventArgs e)
        {
            // Cấu hình DataGridView
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.MultiSelect = false;
            dgvBooks.ReadOnly = true;
            dgvBooks.AllowUserToAddRows = false;

            LoadBooks();
        }

        // LOAD BOOKS
        private void LoadBooks()
        {
            try
            {
                bookList = bookDAL.GetAllBooks();
                dgvBooks.DataSource = null;
                dgvBooks.DataSource = bookList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // BUTTON LOAD
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadBooks();
            MessageBox.Show("Books loaded successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // BUTTON ADD
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;

                Book newBook = new Book
                {
                    BookID = int.Parse(txtBookID.Text),
                    Title = txtTitle.Text,
                    AuthorName = txtAuthor.Text,
                    PublicationYear = int.Parse(txtYear.Text),
                    Price = decimal.Parse(txtPrice.Text),
                    Quantity = int.Parse(txtQuantity.Text)
                };

                bookDAL.InsertBook(newBook);
                LoadBooks();
                ClearFields();
                MessageBox.Show("Book added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding book: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // BUTTON UPDATE
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                try
                {
                    if (!ValidateInputs())
                        return;

                    Book selectedBook = (Book)dgvBooks.SelectedRows[0].DataBoundItem;
                    selectedBook.Title = txtTitle.Text;
                    selectedBook.AuthorName = txtAuthor.Text;
                    selectedBook.PublicationYear = int.Parse(txtYear.Text);
                    selectedBook.Price = decimal.Parse(txtPrice.Text);
                    selectedBook.Quantity = int.Parse(txtQuantity.Text);

                    bookDAL.UpdateBook(selectedBook);
                    LoadBooks();
                    MessageBox.Show("Book updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating book: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to update.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // BUTTON DELETE
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this book?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Book selectedBook = (Book)dgvBooks.SelectedRows[0].DataBoundItem;
                        bookDAL.DeleteBook(selectedBook.BookID);
                        LoadBooks();
                        ClearFields();
                        MessageBox.Show("Book deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting book: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a book to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // DATAGRIDVIEW SELECTION CHANGED
        private void dgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                Book selectedBook = (Book)dgvBooks.SelectedRows[0].DataBoundItem;
                txtBookID.Text = selectedBook.BookID.ToString();
                txtTitle.Text = selectedBook.Title;
                txtAuthor.Text = selectedBook.AuthorName;
                txtYear.Text = selectedBook.PublicationYear.ToString();
                txtPrice.Text = selectedBook.Price.ToString();
                txtQuantity.Text = selectedBook.Quantity.ToString();
            }
        }

        // VALIDATE INPUTS
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtBookID.Text))
            {
                MessageBox.Show("Please enter Book ID", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookID.Focus();
                return false;
            }

            if (!int.TryParse(txtBookID.Text, out int bookId) || bookId <= 0)
            {
                MessageBox.Show("Book ID must be a positive number", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookID.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter book title", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Please enter author name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAuthor.Focus();
                return false;
            }

            if (!int.TryParse(txtYear.Text, out int year) || year < 1000 || year > DateTime.Now.Year + 1)
            {
                MessageBox.Show("Please enter a valid publication year", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtYear.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Please enter a valid price", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid quantity", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            return true;
        }

        // CLEAR FIELDS
        private void ClearFields()
        {
            txtBookID.Clear();
            txtTitle.Clear();
            txtAuthor.Clear();
            txtYear.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtBookID.Focus();
        }
    }
}
