export default {
  getValueOrDefault: (formData: FormData, property: string) => {
    return formData.get(property)?.toString() ?? '';
  }
}