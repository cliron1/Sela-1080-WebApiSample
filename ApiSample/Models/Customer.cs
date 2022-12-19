using ApiSample.Models;

public class Customer {
	public int Id { get; set; }
	public int Name { get; set; }
	
	public List<Pet> Pets { get; set; }
}