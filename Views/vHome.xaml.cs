
using BPUNGUILS5.Models;

namespace BPUNGUILS5.Views;

public partial class vHome : ContentPage
{
	public vHome()
	{
		InitializeComponent();
	}

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        status.Text = "";
        App.personrepo.AddNewPerson(txtNombre.Text);
        status.Text = App.personrepo.StatusMessage;
    }

    private void btnListar_Clicked(object sender, EventArgs e)
    {
        status.Text = "";
        List<Persona> people = App.personrepo.GetAllPersonas();
        ListaPersona.ItemsSource = people;
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        status.Text = "";
        Persona persona = new Persona
        {
            Id = int.Parse(txtId.Text), // Asumiendo que tienes un campo de texto para el ID
            Name = txtNombre.Text
        };
        App.personrepo.UpdatePerson(persona);
        status.Text = App.personrepo.StatusMessage;
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        status.Text = "";
        int personId = int.Parse(txtId.Text); // Asumiendo que tienes un campo de texto para el ID
        App.personrepo.DeletePerson(personId);
        status.Text = App.personrepo.StatusMessage;
    }
}