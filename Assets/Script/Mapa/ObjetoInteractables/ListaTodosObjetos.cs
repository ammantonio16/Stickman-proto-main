
[System.Serializable]
public class ListaTodosObjetos
{
    
    public string nombreObjeto;
    public bool objetoObtenido;
    public int ubiPosButton;
    public int vecesUsadoObjeto;


    public void ListaObjetos(string nombreObjeto, bool objetoObtenido, int UbiPos, int VecesUsado)
    {
        this.nombreObjeto = nombreObjeto;
        this.objetoObtenido = objetoObtenido;
        this.ubiPosButton = UbiPos;
        this.vecesUsadoObjeto = VecesUsado;
    }
}
