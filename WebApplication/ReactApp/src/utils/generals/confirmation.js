import Swal from "sweetalert2";

export default function confirmar(
  onConfirm,
  titulo = "¿Seguro que desea proseguir?",
  textoBotonConfirmacion = "Si",
  textoBotonCancelar = "No"
) {
    Swal.fire({
        title: titulo,
        confirmButtonText: textoBotonConfirmacion,
        cancelButtonText: textoBotonCancelar,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33'
    }).then(result => {
        if (result.isConfirmed) {
            onConfirm()
            Swal.fire('Se completó la opecación!', '', 'success')
          } else {
            Swal.fire('No se ha realizado ningún cambio!', '', 'info')
          }
    })
}
